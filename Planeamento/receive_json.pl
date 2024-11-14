:- use_module(library(http/http_server)).
:- use_module(library(http/http_dispatch)).
:- use_module(library(http/http_json)).

% Define o handler para o endpoint /greet
:- http_handler('/greet', greet_handler, []).

greet_handler(Request) :-
    % Define os cabe�alhos CORS
    format('Content-type: application/json~n'),
    format('Access-Control-Allow-Origin: *~n'),
    format('Access-Control-Allow-Methods: GET, POST, OPTIONS~n'),
    format('Access-Control-Allow-Headers: Content-Type~n'),
    format('~n'),
    (   member(method(options), Request) ->
        true
    ;   % L� o corpo JSON do pedido, se houver
        (   http_read_json_dict(Request, Dict, [default([])])
        ->  Name = Dict.get(name, "World") % Obt�m o campo "name" ou usa "World" por defeito
        ;   Name = "World"
        ),
        format('{"message": "Hello, ~w!"}', [Name])
    ).

% Cria��o de servidor HTTP no porto 'Port'
server(Port) :-
    http_server(http_dispatch, [port(Port)]).
