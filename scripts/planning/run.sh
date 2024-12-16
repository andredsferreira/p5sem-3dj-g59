#!/bin/bash

# Verifica se os parâmetros foram passados corretamente
if [ -z "$3" ]; then
    echo "Usage: $0 'path_to_prolog_executable' 'path_to_script.pl' 'port_number'"
    exit 1
fi

# Parâmetros
# Caminho absoluto até o executável swipl-win.exe
PROLOG_PATH="$1"
# Caminho absoluto até o ficheiro .pl
SCRIPT_PATH="$2"
# Port do servidor http
PORT="$3"

"$PROLOG_PATH" -s "$SCRIPT_PATH" -g "server($PORT), halt."
