﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace LinqToDB.Data
{
	using Linq;

	/// <summary>
	/// Contains extension methods for <see cref="DataConnection"/> class.
	/// </summary>
	[PublicAPI]
	public static class DataConnectionExtensions
	{
		#region SetCommand

		/// <summary>
		/// Creates command wrapper for current connection with provided command text.
		/// </summary>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="commandText">Command text.</param>
		/// <returns>Database command wrapper.</returns>
		public static CommandInfo SetCommand(this DataConnection dataConnection, string commandText)
		{
			return new CommandInfo(dataConnection, commandText);
		}

		/// <summary>
		/// Creates command wrapper for current connection with provided command text and parameters.
		/// </summary>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="commandText">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Database command wrapper.</returns>
		public static CommandInfo SetCommand(DataConnection dataConnection, string commandText, params DataParameter[] parameters)
		{
			return new CommandInfo(dataConnection, commandText, parameters);
		}

		/// <summary>
		/// Creates command wrapper for current connection with provided command text and single parameter.
		/// </summary>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="commandText">Command text.</param>
		/// <param name="parameter">Command parameter.</param>
		/// <returns>Database command wrapper.</returns>
		public static CommandInfo SetCommand(DataConnection dataConnection, string commandText, DataParameter parameter)
		{
			return new CommandInfo(dataConnection, commandText, parameter);
		}

		/// <summary>
		/// Creates command wrapper for current connection with provided command text and parameters.
		/// </summary>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="commandText">Command text.</param>
		/// <param name="parameters">Command parameters. Supported values:
		/// <para> - <c>null</c> for command without parameters;</para>
		/// <para> - single <see cref="DataParameter"/> instance;</para>
		/// <para> - array of <see cref="DataParameter"/> parameters;</para>
		/// <para> - mapping class entity.</para>
		/// <para>Last case will convert all mapped columns to <see cref="DataParameter"/> instances using following logic:</para>
		/// <para> - if column is of <see cref="DataParameter"/> type, column value will be used. If parameter name (<see cref="DataParameter.Name"/>) is not set, column name will be used;</para>
		/// <para> - if converter from column type to <see cref="DataParameter"/> is defined in mapping schema, it will be used to create parameter with colum name passed to converter;</para>
		/// <para> - otherwise column value will be converted to <see cref="DataParameter"/> using column name as parameter name and column value will be converted to parameter value using conversion, defined by mapping schema.</para>
		/// <returns>Database command wrapper.</returns>
		public static CommandInfo SetCommand(DataConnection dataConnection, string commandText, object parameters)
		{
			return new CommandInfo(dataConnection, commandText, parameters);
		}

		#endregion

		#region Query with object reader

		/// <summary>
		/// Executes command and returns results as collection of values, mapped using provided mapping function.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="objectReader">Record mapping function from data reader.</param>
		/// <param name="sql">Command text.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, Func<IDataReader,T> objectReader, string sql)
		{
			return new CommandInfo(connection, sql).Query(objectReader);
		}

		/// <summary>
		/// Executes command using <see cref="CommandType.StoredProcedure"/> command type and returns results as collection of values, mapped using provided mapping function.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="objectReader">Record mapping function from data reader.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> QueryProc<T>(this DataConnection connection, Func<IDataReader,T> objectReader, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).QueryProc(objectReader);
		}

		/// <summary>
		/// Executes command and returns results as collection of values, mapped using provided mapping function.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="objectReader">Record mapping function from data reader.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, Func<IDataReader,T> objectReader, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).Query(objectReader);
		}

		/// <summary>
		/// Executes command and returns results as collection of values, mapped using provided mapping function.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="objectReader">Record mapping function from data reader.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters. Supported values:
		/// <para> - <c>null</c> for command without parameters;</para>
		/// <para> - single <see cref="DataParameter"/> instance;</para>
		/// <para> - array of <see cref="DataParameter"/> parameters;</para>
		/// <para> - mapping class entity.</para>
		/// <para>Last case will convert all mapped columns to <see cref="DataParameter"/> instances using following logic:</para>
		/// <para> - if column is of <see cref="DataParameter"/> type, column value will be used. If parameter name (<see cref="DataParameter.Name"/>) is not set, column name will be used;</para>
		/// <para> - if converter from column type to <see cref="DataParameter"/> is defined in mapping schema, it will be used to create parameter with colum name passed to converter;</para>
		/// <para> - otherwise column value will be converted to <see cref="DataParameter"/> using column name as parameter name and column value will be converted to parameter value using conversion, defined by mapping schema.</para>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, Func<IDataReader,T> objectReader, string sql, object parameters)
		{
			return new CommandInfo(connection, sql, parameters).Query(objectReader);
		}

		#endregion

		#region Query

		/// <summary>
		/// Executes command and returns results as collection of values of specified type.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, string sql)
		{
			return new CommandInfo(connection, sql).Query<T>();
		}

		/// <summary>
		/// Executes command and returns results as collection of values of specified type.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).Query<T>();
		}

		/// <summary>
		/// Executes command using <see cref="CommandType.StoredProcedure"/> command type and returns results as collection of values of specified type.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> QueryProc<T>(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).QueryProc<T>();
		}

		/// <summary>
		/// Executes command and returns results as collection of values of specified type.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameter">Command parameter.</param>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, string sql, DataParameter parameter)
		{
			return new CommandInfo(connection, sql, parameter).Query<T>();
		}

		/// <summary>
		/// Executes command and returns results as collection of values of specified type.
		/// </summary>
		/// <typeparam name="T">Result record type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters. Supported values:
		/// <para> - <c>null</c> for command without parameters;</para>
		/// <para> - single <see cref="DataParameter"/> instance;</para>
		/// <para> - array of <see cref="DataParameter"/> parameters;</para>
		/// <para> - mapping class entity.</para>
		/// <para>Last case will convert all mapped columns to <see cref="DataParameter"/> instances using following logic:</para>
		/// <para> - if column is of <see cref="DataParameter"/> type, column value will be used. If parameter name (<see cref="DataParameter.Name"/>) is not set, column name will be used;</para>
		/// <para> - if converter from column type to <see cref="DataParameter"/> is defined in mapping schema, it will be used to create parameter with colum name passed to converter;</para>
		/// <para> - otherwise column value will be converted to <see cref="DataParameter"/> using column name as parameter name and column value will be converted to parameter value using conversion, defined by mapping schema.</para>
		/// <returns>Returns collection of query result records.</returns>
		public static IEnumerable<T> Query<T>(this DataConnection connection, string sql, object parameters)
		{
			return new CommandInfo(connection, sql, parameters).Query<T>();
		}

		#endregion

		#region Query with template

		/// <summary>
		/// Not implemented. See <see cref="Query{T}(DataConnection, string, DataParameter[])"/>.
		/// </summary>
		public static IEnumerable<T> Query<T>(this DataConnection connection, T template, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).Query(template);
		}

		/// <summary>
		/// Not implemented. See <see cref="Query{T}(DataConnection, string, object)"/>.
		/// </summary>
		public static IEnumerable<T> Query<T>(this DataConnection connection, T template, string sql, object parameters)
		{
			return new CommandInfo(connection, sql, parameters).Query(template);
		}

		#endregion

		#region Execute

		/// <summary>
		/// Executes command and returns number of affected records.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <returns>Number of records, affected by command execution.</returns>
		public static int Execute(this DataConnection connection, string sql)
		{
			return new CommandInfo(connection, sql).Execute();
		}

		/// <summary>
		/// Executes command and returns number of affected records.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Number of records, affected by command execution.</returns>
		public static int Execute(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).Execute();
		}

		/// <summary>
		/// Executes command using <see cref="CommandType.StoredProcedure"/> command type and returns number of affected records.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Number of records, affected by command execution.</returns>
		public static int ExecuteProc(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).ExecuteProc();
		}

		/// <summary>
		/// Executes command and returns number of affected records.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters. Supported values:
		/// <para> - <c>null</c> for command without parameters;</para>
		/// <para> - single <see cref="DataParameter"/> instance;</para>
		/// <para> - array of <see cref="DataParameter"/> parameters;</para>
		/// <para> - mapping class entity.</para>
		/// <para>Last case will convert all mapped columns to <see cref="DataParameter"/> instances using following logic:</para>
		/// <para> - if column is of <see cref="DataParameter"/> type, column value will be used. If parameter name (<see cref="DataParameter.Name"/>) is not set, column name will be used;</para>
		/// <para> - if converter from column type to <see cref="DataParameter"/> is defined in mapping schema, it will be used to create parameter with colum name passed to converter;</para>
		/// <para> - otherwise column value will be converted to <see cref="DataParameter"/> using column name as parameter name and column value will be converted to parameter value using conversion, defined by mapping schema.</para>
		/// <returns>Number of records, affected by command execution.</returns>
		public static int Execute(this DataConnection connection, string sql, object parameters)
		{
			return new CommandInfo(connection, sql, parameters).Execute();
		}

		#endregion

		#region Execute scalar

		/// <summary>
		/// Executes command and returns single value.
		/// </summary>
		/// <typeparam name="T">Resulting value type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <returns>Resulting value.</returns>
		public static T Execute<T>(this DataConnection connection, string sql)
		{
			return new CommandInfo(connection, sql).Execute<T>();
		}

		/// <summary>
		/// Executes command and returns single value.
		/// </summary>
		/// <typeparam name="T">Resulting value type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Resulting value.</returns>
		public static T Execute<T>(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).Execute<T>();
		}

		/// <summary>
		/// Executes command using <see cref="CommandType.StoredProcedure"/> command type and returns single value.
		/// </summary>
		/// <typeparam name="T">Resulting value type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Resulting value.</returns>
		public static T ExecuteProc<T>(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).ExecuteProc<T>();
		}

		/// <summary>
		/// Executes command and returns single value.
		/// </summary>
		/// <typeparam name="T">Resulting value type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameter">Command parameter.</param>
		/// <returns>Resulting value.</returns>
		public static T Execute<T>(this DataConnection connection, string sql, DataParameter parameter)
		{
			return new CommandInfo(connection, sql, parameter).Execute<T>();
		}

		/// <summary>
		/// Executes command and returns single value.
		/// </summary>
		/// <typeparam name="T">Resulting value type.</typeparam>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters. Supported values:
		/// <para> - <c>null</c> for command without parameters;</para>
		/// <para> - single <see cref="DataParameter"/> instance;</para>
		/// <para> - array of <see cref="DataParameter"/> parameters;</para>
		/// <para> - mapping class entity.</para>
		/// <para>Last case will convert all mapped columns to <see cref="DataParameter"/> instances using following logic:</para>
		/// <para> - if column is of <see cref="DataParameter"/> type, column value will be used. If parameter name (<see cref="DataParameter.Name"/>) is not set, column name will be used;</para>
		/// <para> - if converter from column type to <see cref="DataParameter"/> is defined in mapping schema, it will be used to create parameter with colum name passed to converter;</para>
		/// <para> - otherwise column value will be converted to <see cref="DataParameter"/> using column name as parameter name and column value will be converted to parameter value using conversion, defined by mapping schema.</para>
		/// <returns>Resulting value.</returns>
		public static T Execute<T>(this DataConnection connection, string sql, object parameters)
		{
			return new CommandInfo(connection, sql, parameters).Execute<T>();
		}

		#endregion

		#region ExecuteReader

		/// <summary>
		/// Executes command and returns data reader instance.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <returns>Data reader object.</returns>
		public static DataReader ExecuteReader(this DataConnection connection, string sql)
		{
			return new CommandInfo(connection, sql).ExecuteReader();
		}

		/// <summary>
		/// Executes command and returns data reader instance.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Data reader object.</returns>
		public static DataReader ExecuteReader(this DataConnection connection, string sql, params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters).ExecuteReader();
		}

		/// <summary>
		/// Executes command and returns data reader instance.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameter">Command parameter.</param>
		/// <returns>Data reader object.</returns>
		public static DataReader ExecuteReader(this DataConnection connection, string sql, DataParameter parameter)
		{
			return new CommandInfo(connection, sql, parameter).ExecuteReader();
		}

		/// <summary>
		/// Executes command and returns data reader instance.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="parameters">Command parameters. Supported values:
		/// <para> - <c>null</c> for command without parameters;</para>
		/// <para> - single <see cref="DataParameter"/> instance;</para>
		/// <para> - array of <see cref="DataParameter"/> parameters;</para>
		/// <para> - mapping class entity.</para>
		/// <para>Last case will convert all mapped columns to <see cref="DataParameter"/> instances using following logic:</para>
		/// <para> - if column is of <see cref="DataParameter"/> type, column value will be used. If parameter name (<see cref="DataParameter.Name"/>) is not set, column name will be used;</para>
		/// <para> - if converter from column type to <see cref="DataParameter"/> is defined in mapping schema, it will be used to create parameter with colum name passed to converter;</para>
		/// <para> - otherwise column value will be converted to <see cref="DataParameter"/> using column name as parameter name and column value will be converted to parameter value using conversion, defined by mapping schema.</para>
		/// <returns>Data reader object.</returns>
		public static DataReader ExecuteReader(this DataConnection connection, string sql, object parameters)
		{
			return new CommandInfo(connection, sql, parameters).ExecuteReader();
		}

		/// <summary>
		/// Executes command and returns data reader instance.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="sql">Command text.</param>
		/// <param name="commandType">Type of command. See <see cref="CommandType"/> for all supported types.</param>
		/// <param name="commandBehavior">Command behavior flags. See <see cref="CommandBehavior"/> for more details.</param>
		/// <param name="parameters">Command parameters.</param>
		/// <returns>Data reader object.</returns>
		public static DataReader ExecuteReader(
			this DataConnection    connection,
			string                 sql,
			CommandType            commandType,
			CommandBehavior        commandBehavior,
			params DataParameter[] parameters)
		{
			return new CommandInfo(connection, sql, parameters)
			{
				CommandType     = commandType,
				CommandBehavior = commandBehavior,
			}.ExecuteReader();
		}

		#endregion

		#region BulkCopy

		/// <summary>
		/// Performs bulk insert operation.
		/// </summary>
		/// <typeparam name="T">Mapping type of inserted record.</typeparam>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="options">Operation options.</param>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
		public static BulkCopyRowsCopied BulkCopy<T>([NotNull] this DataConnection dataConnection, BulkCopyOptions options, IEnumerable<T> source)
		{
			if (dataConnection == null) throw new ArgumentNullException("dataConnection");
			return dataConnection.DataProvider.BulkCopy(dataConnection, options, source);
		}

		/// <summary>
		/// Performs bulk insert operation.
		/// </summary>
		/// <typeparam name="T">Mapping type of inserted record.</typeparam>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="maxBatchSize">TODO</param>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
		public static BulkCopyRowsCopied BulkCopy<T>([NotNull] this DataConnection dataConnection, int maxBatchSize, IEnumerable<T> source)
		{
			if (dataConnection == null) throw new ArgumentNullException("dataConnection");

			return dataConnection.DataProvider.BulkCopy(
				dataConnection,
				new BulkCopyOptions { MaxBatchSize = maxBatchSize },
				source);
		}

		/// <summary>
		/// Performs bulk insert operation.
		/// </summary>
		/// <typeparam name="T">Mapping type of inserted record.</typeparam>
		/// <param name="dataConnection">Database connection.</param>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
		public static BulkCopyRowsCopied BulkCopy<T>([NotNull] this DataConnection dataConnection, IEnumerable<T> source)
		{
			if (dataConnection == null) throw new ArgumentNullException("dataConnection");

			return dataConnection.DataProvider.BulkCopy(
				dataConnection,
				new BulkCopyOptions(),
				source);
		}

		/// <summary>
		/// Performs bulk intert operation into table specified in <paramref name="options"/> parameter or into table, identified by <paramref name="table"/>.
		/// </summary>
		/// <typeparam name="T">Mapping type of inserted record.</typeparam>
		/// <param name="table">Target table.</param>
		/// <param name="options">Operation options.</param>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
		public static BulkCopyRowsCopied BulkCopy<T>([NotNull] this ITable<T> table, BulkCopyOptions options, IEnumerable<T> source)
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			if (options.TableName    == null) options.TableName    = tbl.TableName;
			if (options.DatabaseName == null) options.DatabaseName = tbl.DatabaseName;
			if (options.SchemaName   == null) options.SchemaName   = tbl.SchemaName;

			return dataConnection.DataProvider.BulkCopy(dataConnection, options, source);
		}

		/// <summary>
		/// Performs bulk intert operation into table, identified by <paramref name="table"/>.
		/// </summary>
		/// <typeparam name="T">Mapping type of inserted record.</typeparam>
		/// <param name="table">Target table.</param>
		/// <param name="maxBatchSize">TODO</param>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
		public static BulkCopyRowsCopied BulkCopy<T>(this ITable<T> table, int maxBatchSize, IEnumerable<T> source)
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			return dataConnection.DataProvider.BulkCopy(
				dataConnection,
				new BulkCopyOptions
				{
					MaxBatchSize = maxBatchSize,
					TableName    = tbl.TableName,
					DatabaseName = tbl.DatabaseName,
					SchemaName   = tbl.SchemaName,
				},
				source);
		}

		/// <summary>
		/// Performs bulk intert operation into table, identified by <paramref name="table"/>.
		/// </summary>
		/// <typeparam name="T">Mapping type of inserted record.</typeparam>
		/// <param name="table">Target table.</param>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
		public static BulkCopyRowsCopied BulkCopy<T>(this ITable<T> table, IEnumerable<T> source)
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			return dataConnection.DataProvider.BulkCopy(
				dataConnection,
				new BulkCopyOptions
				{
					TableName    = tbl.TableName,
					DatabaseName = tbl.DatabaseName,
					SchemaName   = tbl.SchemaName,
				},
				source);
		}

		#endregion

		#region Merge

		public static int Merge<T>(this DataConnection dataConnection, IQueryable<T> source, Expression<Func<T,bool>> predicate,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			return dataConnection.DataProvider.Merge(dataConnection, predicate, true, source.Where(predicate), tableName, databaseName, schemaName);
		}

		public static int Merge<T>(this DataConnection dataConnection, Expression<Func<T,bool>> predicate, IEnumerable<T> source,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			return dataConnection.DataProvider.Merge(dataConnection, predicate, true, source, tableName, databaseName, schemaName);
		}

		public static int Merge<T>(this DataConnection dataConnection, bool delete, IEnumerable<T> source,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			return dataConnection.DataProvider.Merge(dataConnection, null, delete, source, tableName, databaseName, schemaName);
		}

		public static int Merge<T>(this DataConnection dataConnection, IEnumerable<T> source,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			return dataConnection.DataProvider.Merge(dataConnection, null, false, source, tableName, databaseName, schemaName);
		}

		public static int Merge<T>(this ITable<T> table, IQueryable<T> source, Expression<Func<T,bool>> predicate,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			return dataConnection.DataProvider.Merge(dataConnection, predicate, true, source.Where(predicate),
				tableName    ?? tbl.TableName,
				databaseName ?? tbl.DatabaseName,
				schemaName   ?? tbl.SchemaName);
		}

		public static int Merge<T>(this ITable<T> table, Expression<Func<T,bool>> predicate, IEnumerable<T> source,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			return dataConnection.DataProvider.Merge(dataConnection, predicate, true, source,
				tableName    ?? tbl.TableName,
				databaseName ?? tbl.DatabaseName,
				schemaName   ?? tbl.SchemaName);
		}

		public static int Merge<T>(this ITable<T> table, bool delete, IEnumerable<T> source,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			return dataConnection.DataProvider.Merge(dataConnection, null, delete, source,
				tableName    ?? tbl.TableName,
				databaseName ?? tbl.DatabaseName,
				schemaName   ?? tbl.SchemaName);
		}

		public static int Merge<T>(this ITable<T> table, IEnumerable<T> source,
			string tableName = null, string databaseName = null, string schemaName = null)
			where T : class 
		{
			if (table == null) throw new ArgumentNullException("table");

			var tbl            = (Table<T>)table;
			var dataConnection = tbl.DataContext as DataConnection;

			if (dataConnection == null)
				throw new ArgumentException("DataContext must be of DataConnection type.");

			return dataConnection.DataProvider.Merge(dataConnection, null, false, source,
				tableName    ?? tbl.TableName,
				databaseName ?? tbl.DatabaseName,
				schemaName   ?? tbl.SchemaName);
		}

		#endregion
	}
}
