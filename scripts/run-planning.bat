@echo off
:: Verifica se os parâmetros foram passados corretamente
if "%~3"=="" (
    echo Usage: %0 "path_to_prolog_executable" "path_to_script.pl" "port_number"
    exit /b 1
)

:: Parâmetros
:: Caminho absoluto até o executável swipl-win.exe
set PROLOG_PATH=%~1
:: Caminho absoluto até o ficheiro .pl
set SCRIPT_PATH=%~2
:: Port do servidor http
set PORT=%~3

:: Exemplo de chamada -> run-planning.bat "D:\Program Files\swipl\bin\swipl-win.exe" "C:\Users\35193\Desktop\universidade\5Semestre\ALGAV\receive_json\receive_json.pl" 2000

"%PROLOG_PATH%" -s "%SCRIPT_PATH%" -g "server(%PORT%)."
