#!/bin/bash

# 
# Simple pipeline simulation
#

cd ..
dotnet build
dotnet test

cd ExpressBackend
npm install
npm run build
npm run test

cd ../HospitalApp
npm install
npm install three
npm run build
npm run test

cd ../scripts
