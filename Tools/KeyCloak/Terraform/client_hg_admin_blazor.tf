resource "keycloak_openid_client" "hgadminblazor_client" {
  realm_id                     = data.keycloak_realm.hg_realm.id
  client_id                    = var.client_hg_admin_blazor.id
  name                         = "Health Gateway Admin Blazor - ${var.environment.name}"
  description                  = "Health Gateway Blazor Admin web application"
  enabled                      = true
  access_type                  = "PUBLIC"
  login_theme                  = "bcgov"
  standard_flow_enabled        = true
  direct_access_grants_enabled = true
  valid_redirect_uris          = var.client_hg_admin_blazor.valid_redirects
  web_origins                  = var.client_hg_admin_blazor.web_origins
  full_scope_allowed           = false
}

resource "keycloak_openid_client_default_scopes" "hgadminblazor_client_default_scopes" {
  realm_id  = data.keycloak_realm.hg_realm.id
  client_id = keycloak_openid_client.hgadminblazor_client.id
  default_scopes = [
    "profile",
    "web-origins",
    "roles",
    keycloak_openid_client_scope.audience_scope.name,
  ]
}
resource "keycloak_openid_client_optional_scopes" "hgadminblazor_client_optional_scopes" {
  realm_id  = data.keycloak_realm.hg_realm.id
  client_id = keycloak_openid_client.hgadminblazor_client.id
  optional_scopes = [
    "microprofile-jwt",
    "offline_access"
  ]
}

resource "keycloak_generic_role_mapper" "hgadminblazor_adminreviewer" {
  realm_id  = data.keycloak_realm.hg_realm.id
  client_id = keycloak_openid_client.hgadminblazor_client.id
  role_id   = keycloak_role.AdminReviewer.id
}

resource "keycloak_generic_role_mapper" "hgadminblazor_adminuser" {
  realm_id  = data.keycloak_realm.hg_realm.id
  client_id = keycloak_openid_client.hgadminblazor_client.id
  role_id   = keycloak_role.AdminUser.id
}

resource "keycloak_generic_role_mapper" "hgadminblazor_supportuser" {
  realm_id  = data.keycloak_realm.hg_realm.id
  client_id = keycloak_openid_client.hgadminblazor_client.id
  role_id   = keycloak_role.SupportUser.id
}