﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Dynamic;
using Configuratinator3000;

namespace Configuratinator3000
{


public class Config : DynamicObject, IConfiguration
{

	private Config()
	{
		
	}

	private static IConfiguration _config;

	public static IConfiguration Instance
	{
		get { return _config = _config ?? new Config(); }
		set { _config = value; }
	}

	public static void Load()
	{
		Load(ConfigurationManager.AppSettings["Configuration.Environment"]);
	} 

	private Func<IEnumerable<Assembly>> _assemblyLoader = () =>
	                                                      	{
	                                                      		var loadedAssemblies =
	                                                      			AppDomain.CurrentDomain.GetAssemblies().Where(
	                                                      				assembly => !(assembly.IsDynamic || assembly.ReflectionOnly));

	                                                      		var loadedAssemblyPaths =
	                                                      			loadedAssemblies.Select(a => a.Location);

	                                                      		var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

	                                                      		var assemblyFiles =
	                                                      			Directory.GetFiles(baseDirectory, "*.dll",
	                                                      			                   SearchOption.AllDirectories).Where(
	                                                      			                   	f =>
	                                                      			                   	!loadedAssemblyPaths.Contains(f,
	                                                      			                   	                              StringComparer
	                                                      			                   	                              	.
	                                                      			                   	                              	InvariantCultureIgnoreCase));

	                                                      		foreach (var assemblyFile in assemblyFiles)
	                                                      		{
	                                                      			Assembly.Load(AssemblyName.GetAssemblyName(assemblyFile));
	                                                      		}

	                                                      		return
	                                                      			AppDomain.CurrentDomain.GetAssemblies().Where(
	                                                      				ass =>
	                                                      				!(ass.FullName.Contains("System") ||
	                                                      				  ass.FullName.Contains("mscorlib")));
	                                                      	};

	public static void Load(string environment)
	{
		
		Environment = environment ?? "test";
		LoadedAssemblies = GetAssemblies();
	}

	private IEnumerable<Assembly> GetAssemblies()
	{
		return _assemblyLoader();
	}

	public string Environment { get; private set; }

	public IEnumerable<Assembly> LoadedAssemblies { get; private set; }
}
} 
