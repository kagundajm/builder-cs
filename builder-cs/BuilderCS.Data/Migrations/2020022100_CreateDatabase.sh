#!/bin/bash

# Creates builderfs user and database
# builderfs user is granted all privileges on the database
# MUST be run before running migrations for creation of tables

# sudo adduser builderfs --disabled-password

sudo -u postgres psql -c "create database db_name;"

sudo -u postgres psql -c "create user user_name with encrypted password 'user_pwd';"

sudo -u postgres psql -c "grant all privileges on database db_name to user_name;"

sudo -u postgres psql -c "grant all privileges on database builderfs_db to builderfs'"

# sudo -i -u builderfs
# sudo -i -u builderfs psql -d builderfs