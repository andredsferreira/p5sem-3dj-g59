::
:: simple pipeline simulation
::

cd ..
call dotnet build
call dotnet test

cd ExpressBackend
call npm install
call npm run build
call npm run test

cd ../HospitalApp
call npm install
call npm install three
call npm run build
call npm run test

cd ../scripts
