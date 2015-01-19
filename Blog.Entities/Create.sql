CREATE TABLE [User] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[Username]	NVARCHAR(32)	NOT NULL,		
	[Email]		NVARCHAR(64)	NOT NULL,		
	[FullName]	NVARCHAR(256)	NOT NULL,		
	[AvatarUrl]	NVARCHAR(128)	NOT NULL,		
	[RegistrationDate] DATETIME	NOT NULL,		
	UNIQUE([Username]),				
	UNIQUE([Email]),		
	CONSTRAINT [pk_User]		PRIMARY KEY ([Id])		
);		
		
CREATE TABLE [Role] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[Name]		NVARCHAR(16)	NOT NULL,		
	UNIQUE([Name]),		
	CONSTRAINT [pk_Role]		PRIMARY KEY ([Id])		
)		
		
CREATE TABLE [UserRole] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[UserId]	INT				NOT NULL,		
	[RoleId]	INT				NOT NULL,		
	CONSTRAINT [pk_UserRole]	PRIMARY KEY ([Id]),		
	CONSTRAINT [fk_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),		
	CONSTRAINT [fk_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])		
);		
		
CREATE TABLE [Post] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[UserId]	INT				NOT NULL,				
	[Content]	NVARCHAR(2048)	NOT NULL,		
	[Timestamp] DATETIME		NOT NULL,		
	CONSTRAINT [pk_Post]		PRIMARY KEY ([Id]),		
	CONSTRAINT [fk_Post_User]	FOREIGN KEY ([UserId]) REFERENCES [User]([Id])		
);		
		
CREATE TABLE [Article] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),	
	[Title]		NVARCHAR(128)	NOT NULL,	
	[PostId]	INT				NOT NULL,		
	CONSTRAINT [pk_Article]		PRIMARY KEY ([Id]),		
	CONSTRAINT [fk_Article_Post] FOREIGN KEY ([PostId]) REFERENCES [Post]([Id])		
);		
		
CREATE TABLE [Tag] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[Name]		NVARCHAR(32)	NOT NULL,		
	UNIQUE([Name]),		
	CONSTRAINT [pk_Tag]	PRIMARY KEY ([Id]),		
);		
		
CREATE TABLE [ArticleTag] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[ArticleId]	INT				NOT NULL,		
	[TagId]		INT				NOT NULL,		
	CONSTRAINT [pk_ArticleTag]	PRIMARY KEY ([Id]),		
	CONSTRAINT [fk_ArticleTag_Article]	FOREIGN KEY ([ArticleId]) REFERENCES [Article]([Id]),		
	CONSTRAINT [fk_ArticleTag_Tag]		FOREIGN KEY ([TagId]) REFERENCES [Tag]([Id])		
);		
		
CREATE TABLE [Comment] (		
	[Id]		INT				NOT NULL IDENTITY(1,1),		
	[PostId]	INT				NOT NULL,		
	CONSTRAINT [pk_Comment]		PRIMARY KEY ([Id]),		
	CONSTRAINT [fk_Comment_Post] FOREIGN KEY ([PostId]) REFERENCES [Post]([Id])
);

CREATE TABLE [Mark] (
	[Id]		INT				NOT NULL IDENTITY(1,1),
	[UserId]	INT				NOT NULL,
	[PostId]	INT				NOT NULL,
	[Direction]	INT				NOT NULL,
	CONSTRAINT [pk_Mark]		PRIMARY KEY ([Id]),
	CONSTRAINT [fk_Mark_User]	FOREIGN KEY ([UserId]) REFERENCES [Post]([Id]),
	CONSTRAINT [fk_Mark_Post]	FOREIGN KEY ([PostId]) REFERENCES [Post]([Id])
);