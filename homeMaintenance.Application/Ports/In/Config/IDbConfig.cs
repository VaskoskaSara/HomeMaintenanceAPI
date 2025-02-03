﻿using System.Data;

public interface IDbConfig
{
    string DatabaseName { get; }
    string ServerName { get; }
    string Username { get; }
    string Password { get; }
    int ConnectionTimeout { get; }
    IDbConnection GetConnection();
}
