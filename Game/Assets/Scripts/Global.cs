using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using FileManager;

public static class Global {

	public static int Port = 5092;
	public static string Host = "127.0.0.1";
	public static string ConfigurationFilePath = "config.ini";

	public static UdpClient Client { get; private set; }

	public static Config ConfigurationFile { get; private set; }

	[RuntimeInitializeOnLoadMethod]
	public static void Init(){

		ConfigurationFile  = new Config(ConfigurationFilePath); 

		Client = new UdpClient (Host, Port);
		Client.Connect ();
	}
}
