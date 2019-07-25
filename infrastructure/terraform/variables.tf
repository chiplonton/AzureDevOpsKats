variable "region" {
  default = "East US"
}

variable "env_name" {
  default = "test"
}

variable "prject" {
  default = "AzureDevOpsKats"
}

variable "pipeline" {
  default = "AzureContainerServices"
}

variable "ssh_public_key" {
}

variable "service_principal_client_id" {
}

variable "service_principal_client_secret" {
}

variable "dnsimple_token" {}
variable "dnsimple_account" {}

variable "domain_name" { default = "azuredevopskats.com"}
variable "subdomain" {}
