-- Script de criação do schema para PostgreSQL

CREATE TABLE IF NOT EXISTS "Clientes" (
    "Id" SERIAL PRIMARY KEY,
    "NomeCompleto" VARCHAR(100) NOT NULL,
    "CPF" VARCHAR(11) NOT NULL UNIQUE,
    "DataNascimento" TIMESTAMP WITH TIME ZONE NOT NULL,
    "Email" VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS "Dividas" (
    "Id" SERIAL PRIMARY KEY,
    "ClienteId" INTEGER NOT NULL,
    "Valor" DECIMAL(18,2) NOT NULL,
    "Situacao" VARCHAR(10) NOT NULL DEFAULT 'Aberta',
    "DataCriacao" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataPagamento" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "FK_Dividas_Clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES "Clientes" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_Clientes_CPF" ON "Clientes" ("CPF");
CREATE INDEX IF NOT EXISTS "IX_Dividas_ClienteId" ON "Dividas" ("ClienteId");
