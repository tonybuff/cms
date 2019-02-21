IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Menus] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Url] nvarchar(255) NULL,
    [Alias] nvarchar(255) NULL,
    [Icon] nvarchar(128) NULL,
    [ParentGuid] uniqueidentifier NULL,
    [ParentName] nvarchar(max) NULL,
    [Level] int NOT NULL,
    [Description] nvarchar(800) NULL,
    [Sort] int NOT NULL,
    [Status] int NOT NULL,
    [IsDeleted] int NOT NULL,
    [IsDefaultRouter] int NOT NULL,
    [CreatedByUserGuid] uniqueidentifier NOT NULL,
    [CreatedByUserName] nvarchar(max) NULL,
    [ModifiedByUserGuid] uniqueidentifier NULL,
    [ModifiedByUserName] nvarchar(max) NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [IsDeleted] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedByUserGuid] uniqueidentifier NOT NULL,
    [CreatedByUserName] nvarchar(max) NULL,
    [ModifiedOn] datetime2 NULL,
    [ModifiedByUserGuid] uniqueidentifier NULL,
    [ModifiedByUserName] nvarchar(max) NULL,
    [IsSuperAdministrator] bit NOT NULL,
    [IsBuiltin] bit NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Permission] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [MenuId] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [ActionCode] nvarchar(80) NOT NULL,
    [Icon] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [IsDeleted] int NOT NULL,
    [Type] int NOT NULL,
    [CreatedByUserGuid] uniqueidentifier NOT NULL,
    [CreatedByUserName] nvarchar(max) NULL,
    [ModifiedByUserGuid] uniqueidentifier NULL,
    [ModifiedByUserName] nvarchar(max) NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Permission_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [LoginName] nvarchar(50) NOT NULL,
    [DisplayName] nvarchar(50) NULL,
    [Password] nvarchar(255) NULL,
    [Avatar] nvarchar(255) NULL,
    [IsLocked] int NOT NULL,
    [Status] int NOT NULL,
    [IsDeleted] int NOT NULL,
    [CreatedByUserGuid] uniqueidentifier NOT NULL,
    [CreatedByUserName] nvarchar(max) NULL,
    [ModifiedByUserGuid] uniqueidentifier NULL,
    [ModifiedByUserName] nvarchar(max) NULL,
    [Description] nvarchar(800) NULL,
    [RoleId] uniqueidentifier NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [RolePermission] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [PermissionId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_RolePermission] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolePermission_Permission_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permission] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RolePermission_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Permission_MenuId] ON [Permission] ([MenuId]);

GO

CREATE INDEX [IX_RolePermission_PermissionId] ON [RolePermission] ([PermissionId]);

GO

CREATE INDEX [IX_RolePermission_RoleId] ON [RolePermission] ([RoleId]);

GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190121055226_init', N'2.2.0-rtm-35687');

GO

CREATE TABLE [Icons] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [Size] nvarchar(20) NULL,
    [Color] nvarchar(50) NULL,
    [Custom] nvarchar(60) NULL,
    [Description] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [IsDeleted] int NOT NULL,
    [CreatedByUserGuid] uniqueidentifier NOT NULL,
    [CreatedByUserName] nvarchar(max) NULL,
    [ModifiedByUserGuid] uniqueidentifier NULL,
    [ModifiedByUserName] nvarchar(max) NULL,
    CONSTRAINT [PK_Icons] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190128063148_addIconsTable', N'2.2.0-rtm-35687');

GO

ALTER TABLE [Users] ADD [UserType] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190215030516_add-user-column', N'2.2.0-rtm-35687');

GO

DROP INDEX [IX_Users_CreatedByUserGuid] ON [Users];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'CreatedByUserGuid');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [CreatedByUserGuid] uniqueidentifier NULL;

GO

CREATE UNIQUE INDEX [IX_Users_CreatedByUserGuid] ON [Users] ([CreatedByUserGuid]) WHERE [CreatedByUserGuid] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190215065545_add-user-relationship', N'2.2.0-rtm-35687');

GO

