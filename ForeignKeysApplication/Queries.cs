using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeysApplication
{
    public class Queries
    {
        public static string FindAllForeignKeys()
        {
            var query = "SELECT  obj.name AS FK_NAME, "
                            + "sch.name AS[SCHEMA], "
                            + "tab1.name AS[TABLE], "
                            + "col1.name AS[COLUMN], "
                            + "tab2.name AS[REF_TABLE], "
                            + "col2.name AS[REF_COLUMN] "
                      + "FROM sys.foreign_key_columns fkc "
                      + "INNER JOIN sys.objects obj "
                            + "ON obj.object_id = fkc.constraint_object_id "
                      + "INNER JOIN sys.tables tab1 "
                            + "ON tab1.object_id = fkc.parent_object_id "
                      + "INNER JOIN sys.schemas sch "
                            + "ON tab1.schema_id = sch.schema_id "
                      + "INNER JOIN sys.columns col1 "
                            + "ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id "
                      + "INNER JOIN sys.tables tab2 "
                            + "ON tab2.object_id = fkc.referenced_object_id "
                      + "INNER JOIN sys.columns col2 "
                            + "ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id ";

            return query;
        }

        public static string FindForeignKeysWithoutIndexes()
        {
            var query =

            "DECLARE "
            + "@SchemaName varchar(255), "
            + "@TableName varchar(255), "
            + "@ColumnName varchar(255), "
            + "@ForeignKeyName sysname "

            + "SET NOCOUNT ON "
            + "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED "

            + "DECLARE FKColumns_cursor CURSOR Fast_Forward FOR "
            + "SELECT  cu.TABLE_SCHEMA, cu.TABLE_NAME, cu.COLUMN_NAME, cu.CONSTRAINT_NAME "
            + "FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS ic "
            + "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE cu ON ic.CONSTRAINT_NAME = cu.CONSTRAINT_NAME "
            + "WHERE   ic.CONSTRAINT_TYPE = 'FOREIGN KEY' "

            + "CREATE TABLE #temp1("    
            +   "SchemaName varchar(255), "
            +   "TableName varchar(255), "
            +   "ColumnName varchar(255), "
            +   "ForeignKeyName sysname) "

            + "OPEN FKColumns_cursor "
            + "FETCH NEXT FROM FKColumns_cursor INTO @SchemaName, @TableName, @ColumnName, @ForeignKeyName "

            + "WHILE @@FETCH_STATUS = 0 "
            + "BEGIN "

            +    "IF(SELECT COUNT(*) "
            +    "FROM        sysobjects o "
            +       "INNER JOIN sysindexes x ON x.id = o.id "
            +       "INNER JOIN  syscolumns c ON o.id = c.id "
            +       "INNER JOIN sysindexkeys xk ON c.colid = xk.colid AND o.id = xk.id AND x.indid = xk.indid "
            +    "WHERE       o.type in ('U') "
            +       "AND xk.keyno <= x.keycnt "
            +       "AND permissions(o.id, c.name) <> 0 "
            +       "AND(x.status & 32) = 0 "
            +       "AND o.name = @TableName "
            +       "AND c.name = @ColumnName "
            +   ") = 0 "
            +   "BEGIN "
            +        "INSERT INTO #temp1 SELECT @SchemaName, @TableName, @ColumnName, @ForeignKeyName "
            +    "END "


            +   "FETCH NEXT FROM FKColumns_cursor INTO @SchemaName, @TableName, @ColumnName, @ForeignKeyName "
            + "END "
            + "CLOSE FKColumns_cursor "
            + "DEALLOCATE FKColumns_cursor "

            + "SELECT ForeignKeyName as FK, SchemaName as [SCHEMA], TableName as [TABLE], ColumnName as [COLUMN], "
	        +       "'CREATE INDEX IDX_' + ForeignKeyName + ' ON ' + SchemaName + '.' + TableName + '(' + ColumnName + ')' as [QUERY]"
            + "FROM #temp1 " 
            + "ORDER BY TableName "

            + "drop table #temp1";

            return query;
        }
    }
}
