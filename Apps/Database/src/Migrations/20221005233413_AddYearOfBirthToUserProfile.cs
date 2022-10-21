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
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthGateway.Database.Migrations
{
    public partial class AddYearOfBirthToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YearOfBirth",
                schema: "gateway",
                table: "UserProfileHistory",
                type: "character varying(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearOfBirth",
                schema: "gateway",
                table: "UserProfile",
                type: "character varying(4)",
                maxLength: 4,
                nullable: true);

            string schema = "gateway";
            string triggerFunction = @$"
CREATE or REPLACE FUNCTION {schema}.""UserProfileHistoryFunction""()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    NOT LEAKPROOF
AS $BODY$
BEGIN
    IF(TG_OP = 'DELETE') THEN
        INSERT INTO {schema}.""UserProfileHistory""(""UserProfileHistoryId"", ""Operation"", ""OperationDateTime"",
                    ""UserProfileId"", ""AcceptedTermsOfService"", ""TermsOfServiceId"", ""Email"", ""ClosedDateTime"", ""IdentityManagementId"", ""EncryptionKey"",
                    ""YearOfBirth"", ""LastLoginDateTime"", ""CreatedBy"", ""CreatedDateTime"", ""UpdatedBy"", ""UpdatedDateTime"", ""SMSNumber"")
		VALUES(uuid_generate_v4(), TG_OP, now(),
               old.""UserProfileId"", null, old.""TermsOfServiceId"", old.""Email"", old.""ClosedDateTime"", old.""IdentityManagementId"", old.""EncryptionKey"",
               old.""YearOfBirth"", old.""LastLoginDateTime"", old.""CreatedBy"", old.""CreatedDateTime"", old.""UpdatedBy"", old.""UpdatedDateTime"", old.""SMSNumber"");
        RETURN old;
    ELSEIF(TG_OP = 'UPDATE') THEN
        INSERT INTO {schema}.""UserProfileHistory""(""UserProfileHistoryId"", ""Operation"", ""OperationDateTime"",
                    ""UserProfileId"", ""AcceptedTermsOfService"", ""TermsOfServiceId"", ""Email"", ""ClosedDateTime"", ""IdentityManagementId"", ""EncryptionKey"",
                    ""YearOfBirth"", ""LastLoginDateTime"", ""CreatedBy"", ""CreatedDateTime"", ""UpdatedBy"", ""UpdatedDateTime"", ""SMSNumber"")
		VALUES(uuid_generate_v4(), TG_OP || '_LOGIN', now(),
               old.""UserProfileId"", null, old.""TermsOfServiceId"", old.""Email"", old.""ClosedDateTime"", old.""IdentityManagementId"", old.""EncryptionKey"",
               old.""YearOfBirth"", old.""LastLoginDateTime"", old.""CreatedBy"", old.""CreatedDateTime"", old.""UpdatedBy"", old.""UpdatedDateTime"", old.""SMSNumber"");
        RETURN old;
    END IF;
END;$BODY$;";
            migrationBuilder.Sql(triggerFunction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string schema = "gateway";
            string triggerFunction = @$"
CREATE or REPLACE FUNCTION {schema}.""UserProfileHistoryFunction""()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    NOT LEAKPROOF
AS $BODY$
BEGIN
    IF(TG_OP = 'DELETE') THEN
        INSERT INTO {schema}.""UserProfileHistory""(""UserProfileHistoryId"", ""Operation"", ""OperationDateTime"",
                    ""UserProfileId"", ""AcceptedTermsOfService"", ""TermsOfServiceId"", ""Email"", ""ClosedDateTime"", ""IdentityManagementId"",
                    ""EncryptionKey"", ""LastLoginDateTime"", ""CreatedBy"", ""CreatedDateTime"", ""UpdatedBy"", ""UpdatedDateTime"", ""SMSNumber"")
		VALUES(uuid_generate_v4(), TG_OP, now(),
               old.""UserProfileId"", null, old.""TermsOfServiceId"", old.""Email"", old.""ClosedDateTime"", old.""IdentityManagementId"",
               old.""EncryptionKey"", old.""LastLoginDateTime"", old.""CreatedBy"", old.""CreatedDateTime"", old.""UpdatedBy"", old.""UpdatedDateTime"", old.""SMSNumber"");
        RETURN old;
    ELSEIF(TG_OP = 'UPDATE') THEN
        INSERT INTO {schema}.""UserProfileHistory""(""UserProfileHistoryId"", ""Operation"", ""OperationDateTime"",
                    ""UserProfileId"", ""AcceptedTermsOfService"", ""TermsOfServiceId"", ""Email"", ""ClosedDateTime"", ""IdentityManagementId"",
                    ""EncryptionKey"", ""LastLoginDateTime"", ""CreatedBy"", ""CreatedDateTime"", ""UpdatedBy"", ""UpdatedDateTime"", ""SMSNumber"")
		VALUES(uuid_generate_v4(), TG_OP || '_LOGIN', now(),
               old.""UserProfileId"", null, old.""TermsOfServiceId"", old.""Email"", old.""ClosedDateTime"", old.""IdentityManagementId"",
               old.""EncryptionKey"", old.""LastLoginDateTime"", old.""CreatedBy"", old.""CreatedDateTime"", old.""UpdatedBy"", old.""UpdatedDateTime"", old.""SMSNumber"");
        RETURN old;
    END IF;
END;$BODY$;";
            migrationBuilder.Sql(triggerFunction);

            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                schema: "gateway",
                table: "UserProfileHistory");

            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                schema: "gateway",
                table: "UserProfile");
        }
    }
}