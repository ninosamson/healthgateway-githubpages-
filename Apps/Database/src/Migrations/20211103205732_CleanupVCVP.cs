﻿// -------------------------------------------------------------------------
//  Copyright © 2019 Province of British Columbia
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// -------------------------------------------------------------------------
#pragma warning disable CS1591
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthGateway.Database.Migrations
{
    public partial class CleanupVCVP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineProofRequestCache",
                schema: "gateway");

            migrationBuilder.DropTable(
                name: "WalletCredential",
                schema: "gateway");

            migrationBuilder.DropTable(
                name: "VaccineProofTemplateCode",
                schema: "gateway");

            migrationBuilder.DropTable(
                name: "WalletConnection",
                schema: "gateway");

            migrationBuilder.DropTable(
                name: "WalletCredentialStatusCode",
                schema: "gateway");

            migrationBuilder.DropTable(
                name: "WalletConnectionStatusCode",
                schema: "gateway");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VaccineProofTemplateCode",
                schema: "gateway",
                columns: table => new
                {
                    TemplateCode = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineProofTemplateCode", x => x.TemplateCode);
                });

            migrationBuilder.CreateTable(
                name: "WalletConnectionStatusCode",
                schema: "gateway",
                columns: table => new
                {
                    StatusCode = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletConnectionStatusCode", x => x.StatusCode);
                });

            migrationBuilder.CreateTable(
                name: "WalletCredentialStatusCode",
                schema: "gateway",
                columns: table => new
                {
                    StatusCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletCredentialStatusCode", x => x.StatusCode);
                });

            migrationBuilder.CreateTable(
                name: "VaccineProofRequestCache",
                schema: "gateway",
                columns: table => new
                {
                    VaccineProofRequestCacheId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetEndpoint = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiryDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PersonIdentifier = table.Column<string>(type: "character varying(54)", maxLength: 54, nullable: false),
                    ProofTemplate = table.Column<string>(type: "character varying(15)", nullable: false),
                    ShcImageHash = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VaccineProofResponseId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineProofRequestCache", x => x.VaccineProofRequestCacheId);
                    table.UniqueConstraint("AK_VaccineProofRequestCache_PersonIdentifier_ProofTemplate_Shc~", x => new { x.PersonIdentifier, x.ProofTemplate, x.ShcImageHash });
                    table.ForeignKey(
                        name: "FK_VaccineProofRequestCache_VaccineProofTemplateCode_ProofTemp~",
                        column: x => x.ProofTemplate,
                        principalSchema: "gateway",
                        principalTable: "VaccineProofTemplateCode",
                        principalColumn: "TemplateCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletConnection",
                schema: "gateway",
                columns: table => new
                {
                    WalletConnectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConnectedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DisconnectedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InvitationEndpoint = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserProfileId = table.Column<string>(type: "character varying(54)", maxLength: 54, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletConnection", x => x.WalletConnectionId);
                    table.ForeignKey(
                        name: "FK_WalletConnection_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "gateway",
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletConnection_WalletConnectionStatusCode_Status",
                        column: x => x.Status,
                        principalSchema: "gateway",
                        principalTable: "WalletConnectionStatusCode",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletCredential",
                schema: "gateway",
                columns: table => new
                {
                    WalletCredentialId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExchangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<string>(type: "text", nullable: false),
                    ResourceType = table.Column<string>(type: "text", nullable: false),
                    RevocationId = table.Column<string>(type: "text", nullable: true),
                    RevocationRegistryId = table.Column<string>(type: "text", nullable: true),
                    RevokedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    WalletConnectionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletCredential", x => x.WalletCredentialId);
                    table.ForeignKey(
                        name: "FK_WalletCredential_WalletConnection_WalletConnectionId",
                        column: x => x.WalletConnectionId,
                        principalSchema: "gateway",
                        principalTable: "WalletConnection",
                        principalColumn: "WalletConnectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletCredential_WalletCredentialStatusCode_Status",
                        column: x => x.Status,
                        principalSchema: "gateway",
                        principalTable: "WalletCredentialStatusCode",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "gateway",
                table: "VaccineProofTemplateCode",
                columns: new[] { "TemplateCode", "CreatedBy", "CreatedDateTime", "Description", "UpdatedBy", "UpdatedDateTime" },
                values: new object[,]
                {
                    { "BCProvincial", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Provincial template (single page)", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Federal", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Federal template (single page)", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Combined", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Combined Federal and Provincial template", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CombinedCover", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Combined Federal and Provincial template with a cover page", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "gateway",
                table: "WalletConnectionStatusCode",
                columns: new[] { "StatusCode", "CreatedBy", "CreatedDateTime", "Description", "UpdatedBy", "UpdatedDateTime" },
                values: new object[,]
                {
                    { "Pending", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallet Connection is Pending", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Connected", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallet Connection has been created and added", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Disconnected", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallet Connection has been revoked", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "gateway",
                table: "WalletCredentialStatusCode",
                columns: new[] { "StatusCode", "CreatedBy", "CreatedDateTime", "Description", "UpdatedBy", "UpdatedDateTime" },
                values: new object[,]
                {
                    { "Created", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wallet Credential has been created", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Added", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credential has been added to Wallet", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Revoked", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credential has been revoked", "System", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProofRequestCache_ProofTemplate",
                schema: "gateway",
                table: "VaccineProofRequestCache",
                column: "ProofTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_WalletConnection_Status",
                schema: "gateway",
                table: "WalletConnection",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WalletConnection_UserProfileId",
                schema: "gateway",
                table: "WalletConnection",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletCredential_Status",
                schema: "gateway",
                table: "WalletCredential",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WalletCredential_WalletConnectionId",
                schema: "gateway",
                table: "WalletCredential",
                column: "WalletConnectionId");
        }
    }
}