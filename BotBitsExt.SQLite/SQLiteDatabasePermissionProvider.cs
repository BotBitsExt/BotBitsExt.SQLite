using System;
using BotBits.Permissions;
using JetBrains.Annotations;
using SQLite;

namespace BotBitsExt.SQLite
{
    [UsedImplicitly]
    public class SqLiteDatabasePermissionProvider : IPermissionProvider
    {
        private readonly string databasePath;

        public SqLiteDatabasePermissionProvider(string databasePath)
        {
            this.databasePath = databasePath;
            using (var db = new SQLiteConnection(databasePath))
            {
                db.CreateTable<Permissions>();
            }
        }

        public void SetDataAsync(string storageName, PermissionData permissionData)
        {
            using (var db = new SQLiteConnection(databasePath))
            {
                db.InsertOrReplace(new Permissions
                {
                    Username = storageName,
                    Group = permissionData.Group,
                    BanReason = permissionData.BanReason,
                    BanTimeout = permissionData.BanTimeout.Ticks
                });
            }
        }

        public void GetDataAsync(string storageName, Action<PermissionData> callback)
        {
            using (var db = new SQLiteConnection(databasePath))
            {
                var permissions = db.Find<Permissions>(it => it.Username == storageName);
                callback(permissions != null
                    ? new PermissionData(
                        permissions.Group,
                        permissions.BanReason,
                        new DateTime(permissions.BanTimeout))
                    : new PermissionData());
            }
        }
    }
}