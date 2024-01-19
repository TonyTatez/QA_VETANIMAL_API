CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'CAT') THEN
        CREATE SCHEMA "CAT";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'DET') THEN
        CREATE SCHEMA "DET";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'SEG') THEN
        CREATE SCHEMA "SEG";
    END IF;
END $EF$;

CREATE TABLE "CAT"."Catalogs" (
    "IdCatalog" bigint NOT NULL,
    "CatalogName" nvarchar(max) NOT NULL,
    CONSTRAINT "PK_Catalogs" PRIMARY KEY ("IdCatalog")
);

CREATE TABLE "CAT"."Clientes" (
    "IdCliente" bigint NOT NULL,
    "Identificacion" nvarchar(max) NULL,
    "Nombres" nvarchar(max) NULL,
    "Apellidos" nvarchar(max) NULL,
    "Telefono" nvarchar(max) NULL,
    "Correo" nvarchar(max) NULL,
    "Direccion" nvarchar(350) NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_Clientes" PRIMARY KEY ("IdCliente")
);

CREATE TABLE "CAT"."Codigos" (
    "IdCodigosSecuencia" bigint NOT NULL,
    "Codigo" nvarchar(max) NULL,
    "UltimoNumero" int NOT NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_Codigos" PRIMARY KEY ("IdCodigosSecuencia")
);

CREATE TABLE "CAT"."Enfermedad" (
    "IdEnfermedad" bigint NOT NULL,
    "CodigoEnfermedads" nvarchar(max) NULL,
    "Nombre" nvarchar(max) NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_Enfermedad" PRIMARY KEY ("IdEnfermedad")
);

CREATE TABLE "CAT"."Forms" (
    "IdForm" bigint NOT NULL,
    "FormName" nvarchar(max) NOT NULL,
    CONSTRAINT "PK_Forms" PRIMARY KEY ("IdForm")
);

CREATE TABLE "CAT"."QuestionTypes" (
    "IdQuestionType" bigint NOT NULL,
    "QuestionTypeName" nvarchar(max) NOT NULL,
    CONSTRAINT "PK_QuestionTypes" PRIMARY KEY ("IdQuestionType")
);

CREATE TABLE "SEG"."Role" (
    "Id" nvarchar(450) NOT NULL,
    "Name" nvarchar(256) NULL,
    "NormalizedName" nvarchar(256) NULL,
    "ConcurrencyStamp" nvarchar(max) NULL,
    CONSTRAINT "PK_Role" PRIMARY KEY ("Id")
);

CREATE TABLE "CAT"."Sections" (
    "IdSection" bigint NOT NULL,
    "SectionName" nvarchar(max) NOT NULL,
    "IsVisible" bit NOT NULL,
    "IsEnable" bit NOT NULL,
    "IsRequired" bit NOT NULL,
    CONSTRAINT "PK_Sections" PRIMARY KEY ("IdSection")
);

CREATE TABLE "CAT"."Sintomas" (
    "IdSintoma" bigint NOT NULL,
    "Nombre" nvarchar(max) NULL,
    "Descripcion" nvarchar(max) NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_Sintomas" PRIMARY KEY ("IdSintoma")
);

CREATE TABLE "SEG"."Users" (
    "Id" nvarchar(450) NOT NULL,
    "FirstName" nvarchar(max) NULL,
    "LastName" nvarchar(max) NULL,
    "Bloqueo" bit NOT NULL,
    "UserName" nvarchar(256) NULL,
    "NormalizedUserName" nvarchar(256) NULL,
    "Email" nvarchar(256) NULL,
    "NormalizedEmail" nvarchar(256) NULL,
    "EmailConfirmed" bit NOT NULL,
    "PasswordHash" nvarchar(max) NULL,
    "SecurityStamp" nvarchar(max) NULL,
    "ConcurrencyStamp" nvarchar(max) NULL,
    "PhoneNumber" nvarchar(max) NULL,
    "PhoneNumberConfirmed" bit NOT NULL,
    "TwoFactorEnabled" bit NOT NULL,
    "LockoutEnd" datetimeoffset NULL,
    "LockoutEnabled" bit NOT NULL,
    "AccessFailedCount" int NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE TABLE "CAT"."Items" (
    "IdItem" bigint NOT NULL,
    "ItemName" nvarchar(max) NOT NULL,
    "Description" nvarchar(max) NULL,
    "CatalogIdCatalog" bigint NULL,
    CONSTRAINT "PK_Items" PRIMARY KEY ("IdItem"),
    CONSTRAINT "FK_Items_Catalogs_CatalogIdCatalog" FOREIGN KEY ("CatalogIdCatalog") REFERENCES "CAT"."Catalogs" ("IdCatalog")
);

CREATE TABLE "CAT"."Mascotas" (
    "IdMascota" bigint NOT NULL,
    "NombreMascota" nvarchar(max) NULL,
    "Raza" nvarchar(max) NULL,
    "Sexo" nvarchar(max) NULL,
    "FechaNacimiento" datetime2 NOT NULL,
    "IdCliente" bigint NOT NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_Mascotas" PRIMARY KEY ("IdMascota"),
    CONSTRAINT "FK_Mascotas_Clientes_IdCliente" FOREIGN KEY ("IdCliente") REFERENCES "CAT"."Clientes" ("IdCliente") ON DELETE CASCADE
);

CREATE TABLE "CAT"."TipoEnfermedad" (
    "IdTipoEnfermedad" bigint NOT NULL,
    "NombreTipoEnfermedad" nvarchar(max) NULL,
    "ConteoDiagnosticoTipos" int NOT NULL,
    "IdEnfermedad" bigint NULL,
    "EnfermedadIdEnfermedad" bigint NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_TipoEnfermedad" PRIMARY KEY ("IdTipoEnfermedad"),
    CONSTRAINT "FK_TipoEnfermedad_Enfermedad_EnfermedadIdEnfermedad" FOREIGN KEY ("EnfermedadIdEnfermedad") REFERENCES "CAT"."Enfermedad" ("IdEnfermedad")
);

CREATE TABLE "CAT"."Questions" (
    "IdQuestion" bigint NOT NULL,
    "QuestionDesc" nvarchar(max) NOT NULL,
    "Placeholder" nvarchar(max) NULL,
    "QuestionTypeName" nvarchar(max) NOT NULL,
    "IdQuestionType" bigint NOT NULL,
    "IdCatalog" bigint NULL,
    "IsVisible" bit NOT NULL,
    "IsEnable" bit NOT NULL,
    "IsRequired" bit NOT NULL,
    "IsMultiAnswer" bit NOT NULL,
    "MinAnswersCount" int NOT NULL,
    "MaxAnswersCount" int NOT NULL,
    "MinRequiredAnswersCount" int NOT NULL,
    CONSTRAINT "PK_Questions" PRIMARY KEY ("IdQuestion"),
    CONSTRAINT "FK_Questions_Catalogs_IdCatalog" FOREIGN KEY ("IdCatalog") REFERENCES "CAT"."Catalogs" ("IdCatalog"),
    CONSTRAINT "FK_Questions_QuestionTypes_IdQuestionType" FOREIGN KEY ("IdQuestionType") REFERENCES "CAT"."QuestionTypes" ("IdQuestionType") ON DELETE CASCADE
);

CREATE TABLE "SEG"."RoleClaim" (
    "Id" int NOT NULL,
    "RoleId" nvarchar(450) NOT NULL,
    "ClaimType" nvarchar(max) NULL,
    "ClaimValue" nvarchar(max) NULL,
    CONSTRAINT "PK_RoleClaim" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_RoleClaim_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "SEG"."Role" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SEG"."UserClaim" (
    "Id" int NOT NULL,
    "UserId" nvarchar(450) NOT NULL,
    "ClaimType" nvarchar(max) NULL,
    "ClaimValue" nvarchar(max) NULL,
    CONSTRAINT "PK_UserClaim" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UserClaim_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "SEG"."Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SEG"."UserLogin" (
    "LoginProvider" nvarchar(450) NOT NULL,
    "ProviderKey" nvarchar(450) NOT NULL,
    "ProviderDisplayName" nvarchar(max) NULL,
    "UserId" nvarchar(450) NOT NULL,
    CONSTRAINT "PK_UserLogin" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_UserLogin_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "SEG"."Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SEG"."UserRole" (
    "UserId" nvarchar(450) NOT NULL,
    "RoleId" nvarchar(450) NOT NULL,
    CONSTRAINT "PK_UserRole" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_UserRole_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "SEG"."Role" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UserRole_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "SEG"."Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SEG"."UsersToken" (
    "UserId" nvarchar(450) NOT NULL,
    "LoginProvider" nvarchar(450) NOT NULL,
    "Name" nvarchar(450) NOT NULL,
    "Value" nvarchar(max) NULL,
    CONSTRAINT "PK_UsersToken" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_UsersToken_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "SEG"."Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CAT"."CatalogItems" (
    "IdCatalogItem" bigint NOT NULL,
    "IdCatalog" bigint NOT NULL,
    "IdItem" bigint NOT NULL,
    "CatalogOrder" int NOT NULL,
    "ItemOrder" int NOT NULL,
    CONSTRAINT "PK_CatalogItems" PRIMARY KEY ("IdCatalogItem"),
    CONSTRAINT "FK_CatalogItems_Catalogs_IdCatalog" FOREIGN KEY ("IdCatalog") REFERENCES "CAT"."Catalogs" ("IdCatalog") ON DELETE CASCADE,
    CONSTRAINT "FK_CatalogItems_Items_IdItem" FOREIGN KEY ("IdItem") REFERENCES "CAT"."Items" ("IdItem") ON DELETE CASCADE
);

CREATE TABLE "CAT"."FormSectionQuestions" (
    "IdFormSectionQuestion" bigint NOT NULL,
    "IdForm" bigint NOT NULL,
    "IdSection" bigint NOT NULL,
    "IdQuestion" bigint NOT NULL,
    "FormOrder" int NOT NULL,
    "SectionOrder" int NOT NULL,
    "QuestionOrder" int NOT NULL,
    CONSTRAINT "PK_FormSectionQuestions" PRIMARY KEY ("IdFormSectionQuestion"),
    CONSTRAINT "FK_FormSectionQuestions_Forms_IdForm" FOREIGN KEY ("IdForm") REFERENCES "CAT"."Forms" ("IdForm") ON DELETE CASCADE,
    CONSTRAINT "FK_FormSectionQuestions_Questions_IdQuestion" FOREIGN KEY ("IdQuestion") REFERENCES "CAT"."Questions" ("IdQuestion") ON DELETE CASCADE,
    CONSTRAINT "FK_FormSectionQuestions_Sections_IdSection" FOREIGN KEY ("IdSection") REFERENCES "CAT"."Sections" ("IdSection") ON DELETE CASCADE
);

CREATE TABLE "DET"."FichaDetalle" (
    "IdDetalle" bigint NOT NULL,
    "IdFicha" bigint NOT NULL,
    "IdSintoma" bigint NOT NULL,
    "Observacion" nvarchar(max) NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_FichaDetalle" PRIMARY KEY ("IdDetalle"),
    CONSTRAINT "FK_FichaDetalle_Sintomas_IdSintoma" FOREIGN KEY ("IdSintoma") REFERENCES "CAT"."Sintomas" ("IdSintoma") ON DELETE CASCADE
);

CREATE TABLE "DET"."FichaSintoma" (
    "IdFicha" bigint NOT NULL,
    "CodigoFicha" nvarchar(max) NULL,
    "HistoriaClinicaIdHistoriaClinica" bigint NULL,
    "ResultadoIdResultado" bigint NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_FichaSintoma" PRIMARY KEY ("IdFicha")
);

CREATE TABLE "DET"."Resultado" (
    "IdResultado" bigint NOT NULL,
    "IdFicha" bigint NULL,
    "FichaSintomaIdFicha" bigint NULL,
    "FechaResultado" datetime2 NOT NULL,
    "Resultados" nvarchar(max) NULL,
    "Descripcion" nvarchar(max) NULL,
    "IdEnfermedad" nvarchar(max) NULL,
    "EnfermedadIdEnfermedad" bigint NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_Resultado" PRIMARY KEY ("IdResultado"),
    CONSTRAINT "FK_Resultado_Enfermedad_EnfermedadIdEnfermedad" FOREIGN KEY ("EnfermedadIdEnfermedad") REFERENCES "CAT"."Enfermedad" ("IdEnfermedad"),
    CONSTRAINT "FK_Resultado_FichaSintoma_FichaSintomaIdFicha" FOREIGN KEY ("FichaSintomaIdFicha") REFERENCES "DET"."FichaSintoma" ("IdFicha")
);

CREATE TABLE "DET"."HistoriaClinica" (
    "IdHistoriaClinica" bigint NOT NULL,
    "CodigoHistorial" nvarchar(max) NULL,
    "IdMascotas" bigint NOT NULL,
    "ResultadoIdResultado" bigint NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_HistoriaClinica" PRIMARY KEY ("IdHistoriaClinica"),
    CONSTRAINT "FK_HistoriaClinica_Mascotas_IdMascotas" FOREIGN KEY ("IdMascotas") REFERENCES "CAT"."Mascotas" ("IdMascota") ON DELETE CASCADE,
    CONSTRAINT "FK_HistoriaClinica_Resultado_ResultadoIdResultado" FOREIGN KEY ("ResultadoIdResultado") REFERENCES "DET"."Resultado" ("IdResultado")
);

CREATE INDEX "IX_CatalogItems_IdCatalog" ON "CAT"."CatalogItems" ("IdCatalog");

CREATE INDEX "IX_CatalogItems_IdItem" ON "CAT"."CatalogItems" ("IdItem");

CREATE INDEX "IX_FichaDetalle_IdFicha" ON "DET"."FichaDetalle" ("IdFicha");

CREATE INDEX "IX_FichaDetalle_IdSintoma" ON "DET"."FichaDetalle" ("IdSintoma");

CREATE INDEX "IX_FichaSintoma_HistoriaClinicaIdHistoriaClinica" ON "DET"."FichaSintoma" ("HistoriaClinicaIdHistoriaClinica");

CREATE INDEX "IX_FichaSintoma_ResultadoIdResultado" ON "DET"."FichaSintoma" ("ResultadoIdResultado");

CREATE INDEX "IX_FormSectionQuestions_IdForm" ON "CAT"."FormSectionQuestions" ("IdForm");

CREATE INDEX "IX_FormSectionQuestions_IdQuestion" ON "CAT"."FormSectionQuestions" ("IdQuestion");

CREATE INDEX "IX_FormSectionQuestions_IdSection" ON "CAT"."FormSectionQuestions" ("IdSection");

CREATE INDEX "IX_HistoriaClinica_IdMascotas" ON "DET"."HistoriaClinica" ("IdMascotas");

CREATE INDEX "IX_HistoriaClinica_ResultadoIdResultado" ON "DET"."HistoriaClinica" ("ResultadoIdResultado");

CREATE INDEX "IX_Items_CatalogIdCatalog" ON "CAT"."Items" ("CatalogIdCatalog");

CREATE INDEX "IX_Mascotas_IdCliente" ON "CAT"."Mascotas" ("IdCliente");

CREATE INDEX "IX_Questions_IdCatalog" ON "CAT"."Questions" ("IdCatalog");

CREATE INDEX "IX_Questions_IdQuestionType" ON "CAT"."Questions" ("IdQuestionType");

CREATE INDEX "IX_Resultado_EnfermedadIdEnfermedad" ON "DET"."Resultado" ("EnfermedadIdEnfermedad");

CREATE INDEX "IX_Resultado_FichaSintomaIdFicha" ON "DET"."Resultado" ("FichaSintomaIdFicha");

CREATE UNIQUE INDEX "RoleNameIndex" ON "SEG"."Role" ("NormalizedName") WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX "IX_RoleClaim_RoleId" ON "SEG"."RoleClaim" ("RoleId");

CREATE INDEX "IX_TipoEnfermedad_EnfermedadIdEnfermedad" ON "CAT"."TipoEnfermedad" ("EnfermedadIdEnfermedad");

CREATE INDEX "IX_UserClaim_UserId" ON "SEG"."UserClaim" ("UserId");

CREATE INDEX "IX_UserLogin_UserId" ON "SEG"."UserLogin" ("UserId");

CREATE INDEX "IX_UserRole_RoleId" ON "SEG"."UserRole" ("RoleId");

CREATE INDEX "EmailIndex" ON "SEG"."Users" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "SEG"."Users" ("NormalizedUserName") WHERE [NormalizedUserName] IS NOT NULL;

ALTER TABLE "DET"."FichaDetalle" ADD CONSTRAINT "FK_FichaDetalle_FichaSintoma_IdFicha" FOREIGN KEY ("IdFicha") REFERENCES "DET"."FichaSintoma" ("IdFicha") ON DELETE CASCADE;

ALTER TABLE "DET"."FichaSintoma" ADD CONSTRAINT "FK_FichaSintoma_HistoriaClinica_HistoriaClinicaIdHistoriaClinica" FOREIGN KEY ("HistoriaClinicaIdHistoriaClinica") REFERENCES "DET"."HistoriaClinica" ("IdHistoriaClinica");

ALTER TABLE "DET"."FichaSintoma" ADD CONSTRAINT "FK_FichaSintoma_Resultado_ResultadoIdResultado" FOREIGN KEY ("ResultadoIdResultado") REFERENCES "DET"."Resultado" ("IdResultado");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231028061052_AnimalVet.0001', '7.0.12');

COMMIT;

START TRANSACTION;

ALTER TABLE "DET"."FichaSintoma" DROP CONSTRAINT "FK_FichaSintoma_Resultado_ResultadoIdResultado";

ALTER TABLE "DET"."HistoriaClinica" DROP CONSTRAINT "FK_HistoriaClinica_Resultado_ResultadoIdResultado";

ALTER TABLE "DET"."Resultado" DROP CONSTRAINT "FK_Resultado_Enfermedad_EnfermedadIdEnfermedad";

ALTER TABLE "DET"."Resultado" DROP CONSTRAINT "FK_Resultado_FichaSintoma_FichaSintomaIdFicha";

DROP INDEX "DET"."IX_Resultado_EnfermedadIdEnfermedad";

DROP INDEX "DET"."IX_Resultado_FichaSintomaIdFicha";

DROP INDEX "DET"."IX_HistoriaClinica_ResultadoIdResultado";

DROP INDEX "DET"."IX_FichaSintoma_ResultadoIdResultado";

ALTER TABLE "DET"."Resultado" DROP COLUMN "EnfermedadIdEnfermedad";

ALTER TABLE "DET"."Resultado" DROP COLUMN "FechaResultado";

ALTER TABLE "DET"."Resultado" DROP COLUMN "FichaSintomaIdFicha";

ALTER TABLE "DET"."Resultado" DROP COLUMN "Resultados";

ALTER TABLE "DET"."HistoriaClinica" DROP COLUMN "ResultadoIdResultado";

ALTER TABLE "DET"."FichaSintoma" DROP COLUMN "ResultadoIdResultado";

ALTER TABLE "CAT"."Enfermedad" RENAME COLUMN "CodigoEnfermedads" TO "CodigoEnfermedad";

UPDATE "DET"."Resultado" SET "IdFicha" = 0 WHERE "IdFicha" IS NULL;
ALTER TABLE "DET"."Resultado" ALTER COLUMN "IdFicha" SET NOT NULL;
ALTER TABLE "DET"."Resultado" ALTER COLUMN "IdFicha" SET DEFAULT 0;

ALTER TABLE "DET"."Resultado" ALTER COLUMN "IdEnfermedad" TYPE bigint;
UPDATE "DET"."Resultado" SET "IdEnfermedad" = 0 WHERE "IdEnfermedad" IS NULL;
ALTER TABLE "DET"."Resultado" ALTER COLUMN "IdEnfermedad" SET NOT NULL;
ALTER TABLE "DET"."Resultado" ALTER COLUMN "IdEnfermedad" SET DEFAULT 0;

CREATE INDEX "IX_Resultado_IdEnfermedad" ON "DET"."Resultado" ("IdEnfermedad");

CREATE INDEX "IX_Resultado_IdFicha" ON "DET"."Resultado" ("IdFicha");

ALTER TABLE "DET"."Resultado" ADD CONSTRAINT "FK_Resultado_Enfermedad_IdEnfermedad" FOREIGN KEY ("IdEnfermedad") REFERENCES "CAT"."Enfermedad" ("IdEnfermedad") ON DELETE CASCADE;

ALTER TABLE "DET"."Resultado" ADD CONSTRAINT "FK_Resultado_FichaSintoma_IdFicha" FOREIGN KEY ("IdFicha") REFERENCES "DET"."FichaSintoma" ("IdFicha") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231028225254_AnimalVet.0002', '7.0.12');

COMMIT;

START TRANSACTION;

ALTER TABLE "CAT"."Clientes" RENAME COLUMN "Apellidos" TO "Codigo";

ALTER TABLE "CAT"."Mascotas" ALTER COLUMN "FechaNacimiento" DROP NOT NULL;

ALTER TABLE "CAT"."Mascotas" ADD "Codigo" nvarchar(max) NULL;

ALTER TABLE "CAT"."Mascotas" ADD "Peso" real NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231104030130_AnimalVet.0003', '7.0.12');

COMMIT;

START TRANSACTION;

CREATE TABLE "DET"."MotivoConsulta" (
    "IdMotivo" bigint NOT NULL,
    "IdSintoma" bigint NOT NULL,
    "Nombre" nvarchar(max) NULL,
    "Destalle" nvarchar(max) NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_MotivoConsulta" PRIMARY KEY ("IdMotivo")
);

CREATE TABLE "DET"."FichaControl" (
    "IdFichaControl" bigint NOT NULL,
    "CodigoFichaControl" nvarchar(max) NULL,
    "IdHistoriaClinica" bigint NOT NULL,
    "IdMotivo" bigint NOT NULL,
    "Peso" real NOT NULL,
    "Observacion" nvarchar(max) NULL,
    "Activo" bit NOT NULL,
    "FechaRegistro" datetime2 NOT NULL,
    "FechaModificacion" datetime2 NULL,
    "FechaEliminacion" datetime2 NULL,
    "IpRegistro" nvarchar(350) NULL,
    "IpModificacion" nvarchar(350) NULL,
    "IpEliminacion" nvarchar(350) NULL,
    "UsuarioRegistro" nvarchar(350) NULL,
    "UsuarioModificacion" nvarchar(350) NULL,
    "UsuarioEliminacion" nvarchar(350) NULL,
    CONSTRAINT "PK_FichaControl" PRIMARY KEY ("IdFichaControl"),
    CONSTRAINT "FK_FichaControl_HistoriaClinica_IdHistoriaClinica" FOREIGN KEY ("IdHistoriaClinica") REFERENCES "DET"."HistoriaClinica" ("IdHistoriaClinica") ON DELETE CASCADE,
    CONSTRAINT "FK_FichaControl_MotivoConsulta_IdMotivo" FOREIGN KEY ("IdMotivo") REFERENCES "DET"."MotivoConsulta" ("IdMotivo") ON DELETE CASCADE
);

CREATE INDEX "IX_FichaControl_IdHistoriaClinica" ON "DET"."FichaControl" ("IdHistoriaClinica");

CREATE INDEX "IX_FichaControl_IdMotivo" ON "DET"."FichaControl" ("IdMotivo");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231117042935_AnimalVet.0004', '7.0.12');

COMMIT;

START TRANSACTION;

ALTER TABLE "DET"."MotivoConsulta" DROP COLUMN "IdSintoma";

ALTER TABLE "DET"."MotivoConsulta" SET SCHEMA "CAT";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231117060218_AnimalVet.0005', '7.0.12');

COMMIT;

START TRANSACTION;

ALTER TABLE "CAT"."Clientes" ADD "IdUser" nvarchar(max) NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231224075716_AnimalVet.0006', '7.0.12');

COMMIT;

