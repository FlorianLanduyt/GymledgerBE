IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [IsCoach] bit NOT NULL,
        [Firstname] nvarchar(max) NOT NULL,
        [Lastname] nvarchar(max) NOT NULL,
        [BirthDate] datetime2 NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [Exercise] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [GymnastId] nvarchar(450) NULL,
        CONSTRAINT [PK_Exercise] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Exercise_AspNetUsers_GymnastId] FOREIGN KEY ([GymnastId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [GymnastCoach] (
        [GymnastId] nvarchar(450) NOT NULL,
        [CoachId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_GymnastCoach] PRIMARY KEY ([GymnastId], [CoachId]),
        CONSTRAINT [FK_GymnastCoach_AspNetUsers_CoachId] FOREIGN KEY ([CoachId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_GymnastCoach_AspNetUsers_GymnastId] FOREIGN KEY ([GymnastId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [Training] (
        [Id] int NOT NULL IDENTITY,
        [CategoryId] int NULL,
        [Date] datetime2 NOT NULL,
        [Day] nvarchar(max) NULL,
        [FeelingBeforeTraining] int NOT NULL,
        [FeelingAfterTraining] int NOT NULL,
        [GymnastId] nvarchar(450) NULL,
        [AmountOfExercises] int NOT NULL,
        CONSTRAINT [PK_Training] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Training_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Training_AspNetUsers_GymnastId] FOREIGN KEY ([GymnastId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [ExerciseEvaluation] (
        [Id] int NOT NULL IDENTITY,
        [DifficultyScore] int NULL,
        [Note] nvarchar(max) NULL,
        [Series] int NULL,
        [Weight] float NULL,
        [Repetitions] int NULL,
        [TrainingId1] int NULL,
        [ExerciseId] int NULL,
        [TrainingId] int NULL,
        CONSTRAINT [PK_ExerciseEvaluation] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ExerciseEvaluation_Exercise_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ExerciseEvaluation_Training_TrainingId] FOREIGN KEY ([TrainingId]) REFERENCES [Training] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ExerciseEvaluation_Training_TrainingId1] FOREIGN KEY ([TrainingId1]) REFERENCES [Training] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE TABLE [TrainingExercise] (
        [TrainingId] int NOT NULL,
        [ExerciseId] int NOT NULL,
        CONSTRAINT [PK_TrainingExercise] PRIMARY KEY ([TrainingId], [ExerciseId]),
        CONSTRAINT [FK_TrainingExercise_Exercise_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TrainingExercise_Training_TrainingId] FOREIGN KEY ([TrainingId]) REFERENCES [Training] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_Exercise_GymnastId] ON [Exercise] ([GymnastId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_ExerciseEvaluation_ExerciseId] ON [ExerciseEvaluation] ([ExerciseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_ExerciseEvaluation_TrainingId] ON [ExerciseEvaluation] ([TrainingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_ExerciseEvaluation_TrainingId1] ON [ExerciseEvaluation] ([TrainingId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_GymnastCoach_CoachId] ON [GymnastCoach] ([CoachId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_Training_CategoryId] ON [Training] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_Training_GymnastId] ON [Training] ([GymnastId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    CREATE INDEX [IX_TrainingExercise_ExerciseId] ON [TrainingExercise] ([ExerciseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200518142404_changedDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200518142404_changedDb', N'3.1.3');
END;

GO

