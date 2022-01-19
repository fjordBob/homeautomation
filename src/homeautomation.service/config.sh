#!/bin/bash

envsubst < "./appsettings_${APP_NAME}.json" > "./appsettings.json"

