:- dynamic availability/3.
:- dynamic agenda_staff/3.
:- dynamic agenda_staff1/3.
:-dynamic agenda_operation_room/3.
:-dynamic agenda_operation_room1/3.
:-dynamic better_sol/5.
:- dynamic final_time/1.
:-dynamic total_surgery_time/2.
:-dynamic generations/1.
:-dynamic population/1.
:-dynamic prob_crossover/1.
:-dynamic prob_mutation/1.
:-dynamic surgeries_per_room/2.
:-dynamic num_surgeries/1.
:-dynamic best_agenda/3.

:- use_module(library(http/http_server)).
:- use_module(library(http/http_dispatch)).
:- use_module(library(http/http_json)).

:- http_handler('/assign', assign_handler, []).
:- http_handler('/decide', decide_scheduler_handler, []).
:- http_handler('/schedule/best', schedule_best_handler, []).
:- http_handler('/schedule/genetic', schedule_genetic_handler, []).

% Criação do servidor HTTP
server(Port) :- http_server(http_dispatch, [port(Port)]).

schedule_best_handler(Request) :-
    format('Content-type: application/json~n'),
    format('Access-Control-Allow-Origin: *~n'),
    format('Access-Control-Allow-Methods: GET, POST, OPTIONS~n'),
    format('Access-Control-Allow-Headers: Content-Type~n'),
    format('~n'),
    (   member(method(options), Request) ->
        true
    ;   % Le o corpo JSON do pedido, se houver
        (   http_read_json_dict(Request, Dict, [default([])])
        ,   RoomString = Dict.get(room)
        ,   atom_string(Room, RoomString)
        ,   Day = Dict.get(day)
        ,   obtain_better_sol(Room, Day, R, S, T)
        ),
        format('{"room-schedule": "~w",~n"staff-schedules": "~w",~n"final-time": ~w}', [R,S,T])
    ).

decide_scheduler_handler(Request) :-
    format('Content-type: application/json~n'),
    format('Access-Control-Allow-Origin: *~n'),
    format('Access-Control-Allow-Methods: GET, POST, OPTIONS~n'),
    format('Access-Control-Allow-Headers: Content-Type~n'),
    format('~n'),
    (   member(method(options), Request) ->
        true
    ;   % Le o corpo JSON do pedido, se houver
        (   http_read_json_dict(Request, Dict, [default([])])
        ,   RoomString = Dict.get(room)
        ,   atom_string(Room, RoomString)
        ,   Time = Dict.get(timeToUse)
        ,   what_method_to_use(Room, Time, M)
        ),
        format('{"method": "~w"}', [M])
    ).    
    
schedule_genetic_handler(Request) :-
    format('Content-type: application/json~n'),
    format('Access-Control-Allow-Origin: *~n'),
    format('Access-Control-Allow-Methods: GET, POST, OPTIONS~n'),
    format('Access-Control-Allow-Headers: Content-Type~n'),
    format('~n'),
    (   member(method(options), Request) ->
        true
    ;   % Le o corpo JSON do pedido, se houver
        (   http_read_json_dict(Request, Dict, [default([])])
        ,   RoomString = Dict.get(room)
        ,   atom_string(Room, RoomString)
        ,   Day = Dict.get(day)
        ,   NG = Dict.get(num_generations)
        ,   PS = Dict.get(population_size)
        ,   P1 = Dict.get(crossover_chance)
        ,   P2 = Dict.get(mutation_chance)
        ,   generate(Room, Day, NG, PS, P1, P2, R, S, T)
        ),
        format('{"room-schedule": "~w",~n"staff-schedules": "~w",~n"final-time": ~w}', [R,S,T])
    ).

assign_handler(Request) :- 
    format('Content-type: application/json~n'),
    format('Access-Control-Allow-Origin: *~n'),
    format('Access-Control-Allow-Methods: GET, POST, OPTIONS~n'),
    format('Access-Control-Allow-Headers: Content-Type~n'),
    format('~n'),
    (   member(method(options), Request) ->
        true
    ;   % L� o corpo JSON do pedido, se houver
        (   http_read_json_dict(Request, Dict, [default([])])
        ,   Day = Dict.get(day)
        ,   associate_surgeries_to_rooms(Day, O)
        ),
        format('{"surgeries-per-room": "~w"}', [O])
    ).

agenda_staff(d001,20241028,[(1080,1140,c01)]).
agenda_staff(d002,20241028,[(850,900,m02)]).
agenda_staff(d003,20241028,[(760,790,m01)]).
agenda_staff(n001,20241028,[(750,790,m01)]).
agenda_staff(n002,20241028,[(950,980,m02)]).
agenda_staff(n003,20241028,[]).
agenda_staff(d004,20241028,[(950,980,c04)]).
agenda_staff(d005,20241028,[(720,850,m01)]).
agenda_staff(m001,20241028,[]).
agenda_staff(m002,20241028,[]).

timetable(d001,20241028,(280,1200)).
timetable(d002,20241028,(300,1440)).
timetable(d003,20241028,(320,1320)).
timetable(n001,20241028,(300,1200)).
timetable(n002,20241028,(350,1250)).
timetable(n003,20241028,(290,1440)).
timetable(d004,20241028,(420,1020)).
timetable(d005,20241028,(300,1300)).
timetable(m001,20241028,(250,1000)).
timetable(m002,20241028,(390,1440)).

staff(d001,doctor,orthopaedist,[so2,so3,so4]).
staff(d002,doctor,orthopaedist,[so2,so3,so4]).
staff(d003,doctor,orthopaedist,[so2,so3,so4]).
staff(n001,nurse,anaesthetist,[so2,so3,so4]).
staff(n002,nurse,instrumenting,[so2,so3,so4]).
staff(n003,nurse,circulating,[so2,so3,so4]).
staff(d004,doctor,orthopaedist,[so2,so3,so4]).
staff(d005,doctor,anaesthetist,[so2,so3,so4]).
staff(m001,assistant,medicalaction,[so2,so3,so4]).
staff(m002,assistant,medicalaction,[so2,so3,so4]).

%surgery(SurgeryType,TAnesthesia,TSurgery,TCleaning).

surgery(so2,45,60,45).
surgery(so3,45,90,45).
surgery(so4,45,75,45).

surgery_id(so100001,so2).
surgery_id(so100002,so3).
surgery_id(so100003,so4).
surgery_id(so100004,so2).
surgery_id(so100005,so4).
surgery_id(so100006,so2).
surgery_id(so100007,so3).
surgery_id(so100008,so2).
surgery_id(so100009,so2).
surgery_id(so100010,so2).
surgery_id(so100011,so4).
surgery_id(so100012,so2).
surgery_id(so100013,so2).

assignment_surgery(so100001,d001,2).
assignment_surgery(so100001,n001,1).
assignment_surgery(so100001,m001,3).

assignment_surgery(so100002,d002,2).
assignment_surgery(so100002,n002,1).
assignment_surgery(so100002,m002,3).

assignment_surgery(so100003,d003,2).
assignment_surgery(so100003,d005,1).
assignment_surgery(so100003,n003,1).
assignment_surgery(so100003,m001,3).

assignment_surgery(so100004,d001,2).
assignment_surgery(so100004,d002,2).
assignment_surgery(so100004,n001,1).
assignment_surgery(so100004,n002,3).
assignment_surgery(so100004,m002,3).

assignment_surgery(so100005,d002,2).
assignment_surgery(so100005,n001,1).
assignment_surgery(so100005,n002,3).
assignment_surgery(so100005,m001,3).

assignment_surgery(so100006,d004,2).
assignment_surgery(so100006,n002,1).
assignment_surgery(so100006,m002,3).

assignment_surgery(so100007,d002,2).
assignment_surgery(so100007,n002,1).
assignment_surgery(so100007,m002,3).

assignment_surgery(so100008,d001,2).
assignment_surgery(so100008,n001,1).
assignment_surgery(so100008,m001,3).

assignment_surgery(so100009,d002,2).
assignment_surgery(so100009,n002,1).
assignment_surgery(so100009,m002,3).

assignment_surgery(so100010,d003,2).
assignment_surgery(so100010,d005,1).
assignment_surgery(so100010,m001,3).

assignment_surgery(so100011,d004,2).
assignment_surgery(so100011,n003,1).
assignment_surgery(so100011,m001,3).

assignment_surgery(so100012,d001,2).
assignment_surgery(so100012,n001,1).
assignment_surgery(so100012,m002,3).

assignment_surgery(so100013,d002,2).
assignment_surgery(so100013,n002,1).
assignment_surgery(so100013,m002,3).

% surgeries(NSurgeries).
surgeries(13).

% agenda_operation_room(Room,Day,ListOfOccupiedTimes)
agenda_operation_room(or1,20241028,[(520,579,so100000),(1000,1059,so099999)]).
agenda_operation_room(or2,20241028,[(700,800,so200000)]).
agenda_operation_room(or3,20241028,[(750,800,so300000)]).
agenda_operation_room(or4,20241028,[(800,900,so300000),(950,1050,so088888)]).

%------------------------SCHEDULE-SURGERIES-TO-ROOMS--------------------------

associate_surgeries_to_rooms(Day,SurgeriesPerRoom):-
    retractall(total_surgery_time(_,_)),
    retractall(agenda_operation_room1(_,_,_)),
    retractall(surgeries_per_room(_,_)),
    findall(_, (agenda_operation_room(R,D,Agenda), assertz(agenda_operation_room1(R,D,Agenda))), _),
    findall(SurgeryID, surgery_id(SurgeryID, _), LSurgeries),
    getTotalTimes(LSurgeries),
    sort_surgeries_by_time(SortedSurgeries),
    findall(Room, agenda_operation_room1(Room, Day, _), LRooms),
    distribute_surgeries(SortedSurgeries, LRooms, Day),
    findall((R,S), surgeries_per_room(R, S), SurgeriesPerRoom).

getTotalTimes([]):- !.
getTotalTimes([Surgery|TSurgeries]):-
    surgery_id(Surgery, Type),
    surgery(Type, TimeA, TimeS, TimeC),
    Time is TimeA + TimeS + TimeC,
    assertz(total_surgery_time(Surgery, Time)),
    %write("Surgery "), write(Surgery), write(" has time="), write(Time), nl,
    getTotalTimes(TSurgeries).

sort_surgeries_by_time(SortedSurgeries):-
    findall((Time, Surgery), total_surgery_time(Surgery, Time), SurgeriesWithTimes),
    sort(1, @=<, SurgeriesWithTimes, SortedByTime),
    findall(Surgery, member((_, Surgery), SortedByTime), SortedSurgeries).

distribute_surgeries([], _, _):- !.
distribute_surgeries([Surgery|RemainingSurgeries], Rooms, Day):-
    nth1(_, Rooms, Room),
    schedule_surgery(Surgery, Room, Day),
    rotate_list(Rooms, RotatedRooms),
    distribute_surgeries(RemainingSurgeries, RotatedRooms, Day).

schedule_surgery(Surgery, Room, Day):-
    total_surgery_time(Surgery, Duration),
    agenda_operation_room1(Room, Day, Agenda),
    free_agenda0(Agenda, FAgRoom), 
    remove_unf_intervals(Duration, FAgRoom, LAPossibilities),
    schedule_first_interval(Duration, LAPossibilities, (TStart, _)),

    TEnd is TStart + Duration,
    retract(agenda_operation_room1(Room, Day, OldAgenda)),
    insert_agenda((TStart, TEnd, Surgery), OldAgenda, NewAgenda),
    total_room_occupation(NewAgenda, Occupation),
    RoomOccupationRate is Occupation / 1440,
    %write("Occupation Rate for room "),write(Room),write(" = "),write(RoomOccupationRate),nl,
    RoomOccupationRate < 0.8,
    assertz(agenda_operation_room1(Room,Day,NewAgenda)),
    
    ( surgeries_per_room(Room, Operations) ->
        retract(surgeries_per_room(Room, Operations))
    ; 
        Operations = [] % Inicialize como uma lista vazia caso não exista.
    ),
    assertz(surgeries_per_room(Room,[Surgery|Operations])).
    %write("List of surgeries for room "), write(Room), write(" = "), write([Surgery|Operations]),nl,
    %write("Scheduled "), write(Surgery), write(" (with duration="), write(Duration), write(") in room "), write(Room), nl,
    %write("Updated room "), write(Room), write("'s agenda: "), write(NewAgenda), nl.

total_room_occupation([],0).
total_room_occupation([(Start,End,_)|T],Occupation):-
    total_room_occupation(T,TOccupation),
    Occupation is TOccupation + End - Start.

%-------------------------------------DECIDE-METHOD--------------------------------------------------------

calculate_f(X, F) :-
    F is (2.29 * 10**(-7) * exp(2 * (X+3))).
    % Function that represents the obtain best equation. (X = nº of surgeries, Y = nº of seconds needed)

what_method_to_use(Room, Time, Method) :-
    surgeries_per_room(Room, Surgeries),
    length(Surgeries, X),
    calculate_f(X, F),
    (Time > F -> 
        Method = obtain_better_sol % Use the "obtain_better_sol" predicate
    ; Time < 0.2 * X -> 
        Method = heuristic % Must use heuristic, since user has little time
    ; 
        P is ceil(Time * 10) // X,
        G is P // 2,
        format(atom(Method), 'generate (Population = ~w, Generations = ~w)', [P, G])
    ).

%--------------------------------------OBTAIN-BEST---------------------------------------------------------

schedule_all_surgeries(Room,Day):-
    retractall(agenda_staff1(_,_,_)),
    retractall(agenda_operation_room1(_,_,_)),
    retractall(availability(_,_,_)),
    findall(_,(agenda_staff(D,Day,Agenda),assertz(agenda_staff1(D,Day,Agenda))),_),
    agenda_operation_room(Or,Date,Agenda),assert(agenda_operation_room1(Or,Date,Agenda)),
    findall(_,(agenda_staff1(D,Date,L),free_agenda0(L,LFA),adapt_timetable(D,Date,LFA,LFA2),assertz(availability(D,Date,LFA2))),_),
    %findall(OpCode,surgeries_per_room(Room,OpCode),LOpCode),
    surgeries_per_room(Room,LOpCode),

    availability_all_surgeries(LOpCode,Room,Day),!.

availability_all_surgeries([], _, _).
availability_all_surgeries([OpCode | LOpCode], Room, Day) :-
    schedule_phases(OpCode, Room, Day),
    availability_all_surgeries(LOpCode, Room, Day).

schedule_phases(OpCode, Room, Day) :-
    surgery_id(OpCode, OpType),
    surgery(OpType, TAnaesthesia, TSurgery, TCleaning),
    TotalDuration is TAnaesthesia + TSurgery + TCleaning,
    findall(Staff, assignment_surgery(OpCode, Staff, _), LStaff),
    intersect_all_agendas(LStaff, Day, LA),

    agenda_operation_room1(Room, Day, LAgendaRoom),
    free_agenda0(LAgendaRoom, LFAgRoom),
    intersect_2_agendas(LA, LFAgRoom, LIntADoctorsRoom),
    remove_unf_intervals(TotalDuration, LIntADoctorsRoom, LAPossibilities),
    schedule_first_interval(TotalDuration, LAPossibilities, (TStart, _)),


    TEndAnaesthesia is TStart + TAnaesthesia,
    TEndSurgery is TEndAnaesthesia + TSurgery,
    TEndCleaning is TEndSurgery + TCleaning,
    retract(agenda_operation_room1(Room, Day, Agenda)),
    insert_agenda((TStart, TEndAnaesthesia, OpCode), Agenda, Agenda1),
    insert_agenda((TEndAnaesthesia, TEndSurgery, OpCode), Agenda1, Agenda2),
    insert_agenda((TEndSurgery, TEndCleaning, OpCode), Agenda2, Agenda3),
    assertz(agenda_operation_room1(Room, Day, Agenda3)),

    findall(Staff, assignment_surgery(OpCode, Staff, 1), LAStaff),
    insert_agenda_doctors((TStart, TEndAnaesthesia, OpCode), Day, LAStaff),
    findall(Staff, assignment_surgery(OpCode, Staff, 2), LSStaff),
    insert_agenda_doctors((TEndAnaesthesia, TEndSurgery, OpCode), Day, LSStaff),
    findall(Staff, assignment_surgery(OpCode, Staff, 3), LCStaff),
    insert_agenda_doctors((TEndSurgery, TEndCleaning, OpCode), Day, LCStaff).

obtain_better_sol(Room,Day,AgOpRoomBetter,LAgDoctorsBetter,TFinOp):-
		%get_time(Ti),
		(obtain_better_sol1(Room,Day);true),
		retract(better_sol(Day,Room,AgOpRoomBetter,LAgDoctorsBetter,TFinOp)).
            %write('Final Result: AgOpRoomBetter='),write(AgOpRoomBetter),nl,
            %write('LAgDoctorsBetter='),write(LAgDoctorsBetter),nl,
            %write('TFinOp='),write(TFinOp),nl,
		%get_time(Tf),
		%T is Tf-Ti,
		%write('Tempo de geracao da solucao:'),write(T),nl.


obtain_better_sol1(Room,Day):-
    asserta(better_sol(Day,Room,_,_,1441)),
    %findall(OpCode,surgery_id(OpCode,_),LOC),!,
    surgeries_per_room(Room,LOC),!,
    %write("LOC="),write(LOC),nl,
    permutation(LOC,LOpCode),
    retractall(agenda_staff1(_,_,_)),
    retractall(agenda_operation_room1(_,_,_)),
    retractall(availability(_,_,_)),
    findall(_,(agenda_staff(D,Day,Agenda),assertz(agenda_staff1(D,Day,Agenda))),_),
    agenda_operation_room(Room,Day,Agenda),assert(agenda_operation_room1(Room,Day,Agenda)),
    findall(_,(agenda_staff1(D,Day,L),free_agenda0(L,LFA),adapt_timetable(D,Day,LFA,LFA2),assertz(availability(D,Day,LFA2))),_),
    availability_all_surgeries(LOpCode,Room,Day),
    agenda_operation_room1(Room,Day,AgendaR),
		update_better_sol(Day,Room,AgendaR,LOpCode),
		fail.

update_better_sol(Day,Room,Agenda,LOpCode):-
                %Evaluation1
                better_sol(Day,Room,_,_,FinTime),
                reverse(Agenda,AgendaR),
                evaluate_final_time(AgendaR,LOpCode,FinTime1),
             %write('Analysing for LOpCode='),write(LOpCode),nl,
             %write('now: FinTime1='),write(FinTime1),write(' FinTime='),write(FinTime),nl,
                FinTime1<FinTime,
             %write('best solution updated'),nl,
                retract(better_sol(_,_,_,_,_)),
                findall(Doctor,assignment_surgery(_,Doctor,_),LDoctors1),
                remove_equals(LDoctors1,LDoctors),
                list_doctors_agenda(Day,LDoctors,LDAgendas),
		asserta(better_sol(Day,Room,Agenda,LDAgendas,FinTime1)).

                %Evaluation2
                %better_sol(Day,Room,_,_,BestAvg),
                %evaluate_average_occupation(Day, Avg),
             %write('Analysing for LOpCode='),write(LOpCode),nl,
             %write('now: Avg='),write(Avg),write(' BestAvg='),write(BestAvg),nl,
		%Avg<BestAvg,
             %write('best solution updated'),nl,
                %retract(better_sol(_,_,_,_,_)),
                %findall(Doctor,assignment_surgery(_,Doctor,_),LDoctors1),
                %remove_equals(LDoctors1,LDoctors),
                %list_doctors_agenda(Day,LDoctors,LDAgendas),
		%asserta(better_sol(Day,Room,Agenda,LDAgendas,Avg)).

% -------------------------------GENETIC-------------------------------------





% parameters initialization
initialize(NG,PS,P1,P2):-
    %write('Number of new generations: '),read(NG), 			
    (retract(generations(_));true), asserta(generations(NG)),
	%write('Population size: '),read(PS),
	(retract(population(_));true), asserta(population(PS)),
	%write('Probability of crossover (%):'), read(P1),
	PC is P1/100, 
	(retract(prob_crossover(_));true), 	asserta(prob_crossover(PC)),
	%write('Probability of mutation (%):'), read(P2),
	PM is P2/100, 
	(retract(prob_mutation(_));true), asserta(prob_mutation(PM)).





% --- MAIN 

generate(Room,Day,NG,PS,P1,P2,AgendaRoom,AgendaDoctors,BestTime):-
    initialize(NG,PS,P1,P2),
    generate_population(Pop,Room),
    %write('Pop='),write(Pop),nl,
    evaluate_population(Pop,PopValue,Room,Day,inf),
    %write('PopValue='),write(PopValue),nl,
    order_population(PopValue,PopOrd),
    generations(NG),
    generate_generation(0,NG,PopOrd,Room,Day),
    retract(better_sol(Day,Room,AgendaRoom,AgendaDoctors,BestTime)).






% --- Generate population

generate_population(Pop,Room):-
    population(PopSize),
    surgeries_per_room(Room,SurgeryList),
    length(SurgeryList,NumT),
    retractall(availability(_,_,_)),
    retractall(num_surgeries(_)),
    assertz(num_surgeries(NumT)),
    generate_population(PopSize,SurgeryList,NumT,Pop).

generate_population(0,_,_,[]):-!.
generate_population(PopSize,SurgeryList,NumT,[Ind|Rest]):-
    PopSize1 is PopSize-1,
    generate_population(PopSize1,SurgeryList,NumT,Rest),
    generate_individual(SurgeryList,NumT,Ind),
    not(member(Ind,Rest)).
generate_population(PopSize,SurgeryList,NumT,L):-
    generate_population(PopSize,SurgeryList,NumT,L).

% --- Generate individual (auxiliar de generate_population)

generate_individual([G],1,[G]):-!.
generate_individual(SurgeryList,NumT,[G|Rest]):-
    NumTemp is NumT + 1, % to use with random
    random(1,NumTemp,N),
    remove(N,SurgeryList,G,NewList),
    NumT1 is NumT-1,
    generate_individual(NewList,NumT1,Rest).

remove(1,[G|Rest],G,Rest).
remove(N,[G1|Rest],G,[G1|Rest1]):- N1 is N-1,remove(N1,Rest,G,Rest1).






% --- Fitness calculation (avaliar)

evaluate_population([], [], _, _, _).
evaluate_population([Ind | Rest], [Ind*V | Rest1], Room, Day, BestV) :-
    evaluate(Ind, V, Room, Day),
    ( BestV = inf ->
        findall(Doctor,assignment_surgery(_,Doctor,_),LDoctors1),
        remove_equals(LDoctors1,LDoctors),
        list_doctors_agenda(Day,LDoctors,LDAgendas),
        agenda_operation_room1(Room, Day, Agenda),
		asserta(better_sol(Day,Room,Agenda,LDAgendas,V)),
        NewBestV = V
    ; 
    ( V < BestV ->
        retract(better_sol(_,_,_,_,_)),
        findall(Doctor,assignment_surgery(_,Doctor,_),LDoctors1),
        remove_equals(LDoctors1,LDoctors),
        list_doctors_agenda(Day,LDoctors,LDAgendas),
        agenda_operation_room1(Room, Day, Agenda),
		asserta(better_sol(Day,Room,Agenda,LDAgendas,V)),
        NewBestV = V
    ;
        NewBestV = BestV
    )),
    evaluate_population(Rest, Rest1, Room, Day, NewBestV).

evaluate(Seq,V,Room,Day):- 
    retractall(agenda_staff1(_,_,_)),
    retractall(agenda_operation_room1(_,_,_)),
    retractall(availability(_,_,_)),
    findall(_,(agenda_staff(D,Day,Agenda),assertz(agenda_staff1(D,Day,Agenda))),_),
    agenda_operation_room(Room,Day,Agenda),assert(agenda_operation_room1(Room,Day,Agenda)),
    findall(_,(agenda_staff1(D,Day,L),free_agenda0(L,LFA),adapt_timetable(D,Day,LFA,LFA2),assertz(availability(D,Day,LFA2))),_),
    schedule_operations(Seq,Room,Day),
    agenda_operation_room1(Room,Day,NewAgenda),
    reverse(NewAgenda,AgendaR),
    evaluate_final_time(AgendaR,Seq,V).


schedule_operations([],_,_).
schedule_operations([Surgery|TOp],Room,Day):-
    surgery_id(Surgery,Type),
    surgery(Type, TA,TS,TC),
    Duration is TA+TS+TC,
    findall(Staff, assignment_surgery(Surgery, Staff, _), LStaff),
    intersect_all_agendas(LStaff, Day, LA),

    agenda_operation_room1(Room, Day, Agenda),
    free_agenda0(Agenda, FAgRoom),
    intersect_2_agendas(LA, FAgRoom, LIntADoctorsRoom),
    remove_unf_intervals(Duration, LIntADoctorsRoom, LAPossibilities),
    schedule_first_interval(Duration, LAPossibilities, (TStart, _)),

    TEndAnaesthesia is TStart + TA,
    TEndSurgery is TEndAnaesthesia + TS,
    TEndCleaning is TEndSurgery + TC,
    retract(agenda_operation_room1(Room, Day, Agenda)),
    insert_agenda((TStart, TEndAnaesthesia, Surgery), Agenda, Agenda1),
    insert_agenda((TEndAnaesthesia, TEndSurgery, Surgery), Agenda1, Agenda2),
    insert_agenda((TEndSurgery, TEndCleaning, Surgery), Agenda2, Agenda3),
    assertz(agenda_operation_room1(Room, Day, Agenda3)),

    findall(Staff, assignment_surgery(Surgery, Staff, 1), LAStaff),
    insert_agenda_doctors((TStart, TEndAnaesthesia, Surgery), Day, LAStaff),
    findall(Staff, assignment_surgery(Surgery, Staff, 2), LSStaff),
    insert_agenda_doctors((TEndAnaesthesia, TEndSurgery, Surgery), Day, LSStaff),
    findall(Staff, assignment_surgery(Surgery, Staff, 3), LCStaff),
    insert_agenda_doctors((TEndSurgery, TEndCleaning, Surgery), Day, LCStaff),
    schedule_operations(TOp,Room,Day).

order_population(PopValue,PopValueOrd):-
    bsort(PopValue,PopValueOrd).

bsort([X],[X]):-!.
bsort([X|Xs],Ys):-
    bsort(Xs,Zs),
    bchange([X|Zs],Ys).


bchange([X],[X]):-!.
bchange([X*VX,Y*VY|L1],[Y*VY|L2]):-
    VX>VY,!,
    bchange([X*VX|L1],L2).

bchange([X|L1],[X|L2]):-bchange(L1,L2).





% --- Gerar nova populacao depois de fazer o crossover, mutacao e avaliacao dos novos individuos

generate_generation(G,G,_,_,_):-!.
	%write('Generation '), write(G), write(':'), nl, write(Pop), nl.
generate_generation(N,G,Pop,Room,Day):-
	%write('Generation '), write(N), write(':'), nl, write(Pop), nl,
    Pop = [Best|_], % Extract the best element
    %write('Best Element: '), write(Best), nl, % Write the first element
    crossover(Pop,NPop1),
	mutation(NPop1,NPop),
    evaluate_population(NPop,NPopValue,Room,Day,inf),
	order_population(NPopValue,NPopOrd),
    change_if_not_present(Best, NPopOrd, NPopFinal), 
	order_population(NPopFinal,NPopOrd2),
	N1 is N+1,
	generate_generation(N1,G,NPopOrd2,Room,Day).

generate_crossover_points(P1,P2):- generate_crossover_points1(P1,P2).
generate_crossover_points1(P1,P2):-
	num_surgeries(N),
	NTemp is N+1,
	random(1,NTemp,P11),
	random(1,NTemp,P21),
	P11\==P21,!,
	((P11<P21,!,P1=P11,P2=P21);P1=P21,P2=P11).
generate_crossover_points1(P1,P2):- generate_crossover_points1(P1,P2).





% --- Crossover

crossover([ ],[ ]).
crossover([Ind*_],[Ind]).
crossover([Ind1*_,Ind2*_|Rest],[NInd1,NInd2|Rest1]):-
	generate_crossover_points(P1,P2),
	prob_crossover(Pcruz),random(0.0,1.0,Pc),
	((Pc =< Pcruz,!,
        cross(Ind1,Ind2,P1,P2,NInd1),
	  cross(Ind2,Ind1,P1,P2,NInd2))
	;
	(NInd1=Ind1,NInd2=Ind2)),
	crossover(Rest,Rest1).

fillh([ ],[ ]).

fillh([_|R1],[h|R2]):-
	fillh(R1,R2).

sublist(L1,I1,I2,L):-I1 < I2,!,
    sublist1(L1,I1,I2,L).

sublist(L1,I1,I2,L):-sublist1(L1,I2,I1,L).

sublist1([X|R1],1,1,[X|H]):-!, fillh(R1,H).

sublist1([X|R1],1,N2,[X|R2]):-!,N3 is N2 - 1,
	sublist1(R1,1,N3,R2).

sublist1([_|R1],N1,N2,[h|R2]):-N3 is N1 - 1,
		N4 is N2 - 1,
		sublist1(R1,N3,N4,R2).

rotate_right(L,K,L1):- surgeries(N),
	T is N - K,
	rr(T,L,L1).

rr(0,L,L):-!.

rr(N,[X|R],R2):- N1 is N - 1,
	append(R,[X],R1),
	rr(N1,R1,R2).

remove([],_,[]):-!.

remove([X|R1],L,[X|R2]):- not(member(X,L)),!,
        remove(R1,L,R2).

remove([_|R1],L,R2):-
    remove(R1,L,R2).

insert([],L,_,L):-!.
insert([X|R],L,N,L2):-
    num_surgeries(T),
    ((N>T,!,N1 is N mod T);N1 = N),
    insert1(X,N1,L,L1),
    N2 is N + 1,
    insert(R,L1,N2,L2).


insert1(X,1,L,[X|L]):-!.
insert1(X,N,[Y|L],[Y|L1]):-
    N1 is N-1,
    insert1(X,N1,L,L1).

cross(Ind1,Ind2,P1,P2,NInd11):-
    sublist(Ind1,P1,P2,Sub1),
    num_surgeries(NumT),
    R is NumT-P2,
    rotate_right(Ind2,R,Ind21),
    remove(Ind21,Sub1,Sub2),
    P3 is P2 + 1,
    insert(Sub2,Sub1,P3,NInd1),
    removeh(NInd1,NInd11).


removeh([],[]).

removeh([h|R1],R2):-!,
    removeh(R1,R2).

removeh([X|R1],[X|R2]):-
    removeh(R1,R2).





% --- Mutação

mutation([],[]).
mutation([Ind|Rest],[NInd|Rest1]):-
	prob_mutation(Pmut),
	random(0.0,1.0,Pm),
	((Pm < Pmut,!,mutacao1(Ind,NInd));NInd = Ind),
	mutation(Rest,Rest1).

mutacao1(Ind,NInd):-
	generate_crossover_points(P1,P2),
	mutacao22(Ind,P1,P2,NInd).

mutacao22([G1|Ind],1,P2,[G2|NInd]):-
	!, P21 is P2-1,
	mutacao23(G1,P21,Ind,G2,NInd).
mutacao22([G|Ind],P1,P2,[G|NInd]):-
	P11 is P1-1, P21 is P2-1,
	mutacao22(Ind,P11,P21,NInd).

mutacao23(G1,1,[G2|Ind],G2,[G1|Ind]):-!.
mutacao23(G1,P,[G|Ind],G2,[G|NInd]):-
	P1 is P-1,
	mutacao23(G1,P1,Ind,G2,NInd).


% If Element is in the list, simply return the original list.
change_if_not_present(Element, List, List) :-
    member(Element, List), !.

% If Element is not in the list, remove the last element and add Element to the end.
change_if_not_present(Element, List, ReturnedList) :-
    remove_last(List, RemovedList),
    add_Best(Element, RemovedList, ReturnedList).

% Remove the last element of a list.
remove_last([_], []).                  % Base case: Single-element list.
remove_last([H|T], [H|Result]) :-      % Recursive case: Process the rest.
    remove_last(T, Result).

% Add an element to the end of a list.
add_Best(Element, [], [Element]). % Base case: Adding to an empty list.
add_Best(Element, [Head|Tail], [Head|NewTail]) :-
    add_Best(Element, Tail, NewTail). % Recursive case: Add to the rest.


% Shuffle the list randomly
shuffle(List, Shuffled) :-
    random_permutation(List, Shuffled).    

% -------------------------------Evaluation1------------------------------


evaluate_final_time([],_,1441).
evaluate_final_time([(_,Tfin,OpCode)|_],LOpCode,Tfin):-member(OpCode,LOpCode),!.
evaluate_final_time([_|AgR],LOpCode,Tfin):-evaluate_final_time(AgR,LOpCode,Tfin).

list_doctors_agenda(_,[],[]).
list_doctors_agenda(Day,[D|LD],[(D,AgD)|LAgD]):-agenda_staff1(D,Day,AgD),list_doctors_agenda(Day,LD,LAgD).

remove_equals([],[]).
remove_equals([X|L],L1):-member(X,L),!,remove_equals(L,L1).
remove_equals([X|L],[X|L1]):-remove_equals(L,L1).

% ------------------------------Evaluation2------------------------------

evaluate_average_occupation(Day, AvgOccupation):-
    findall(StaffID, staff(StaffID, _, _, _), Staffs),
    get_staff_occupation(Day,Staffs,Occupations,Count),
    sum_list(Occupations,TotalOccupation),
    %write('TotalOccupation='),write(TotalOccupation),nl,
    %write('Count='),write(Count),nl,
    AvgOccupation is TotalOccupation / Count.

get_staff_occupation(_,[],[],0).
get_staff_occupation(Day,[H|T],[HOccupation|TOccupation],Count):-
    get_staff_occupation(Day,T,TOccupation,Count1),
    Count is Count1+1,
    agenda_staff1(H,Day,Agenda),
    %write('Agenda for '), write(H), write('= '), write(Agenda),nl,
    sum_occupation(Agenda,HOccupation).

sum_occupation([(HStart,HEnd,_)|T], Occupation) :-
    get_min_start_max_end(T, HStart, HEnd, MinStart, MaxEnd),
    Occupation is MaxEnd - MinStart.

get_min_start_max_end([], CurrentMinStart, CurrentMaxEnd, CurrentMinStart, CurrentMaxEnd).
get_min_start_max_end([(HStart,HEnd,_)|T], CurrentMinStart, CurrentMaxEnd, MinStart, MaxEnd) :-
    NewMinStart is min(CurrentMinStart, HStart),
    NewMaxEnd is max(CurrentMaxEnd, HEnd),
    get_min_start_max_end(T, NewMinStart, NewMaxEnd, MinStart, MaxEnd).



% ------------------------------Heuristic1-------------------------------

heuristic_schedule_all(Room, Day) :-
    get_time(Ti),
    findall(OpCode,surgery_id(OpCode,_),LOpCode),

    try_each_permutation(Room, Day, LOpCode),

    final_time(TFinal),
    get_time(Tf),
    T is Tf - Ti,
    write('Escalonamento conclu�do. Tempo final da �ltima opera��o: '), write(TFinal), nl, write('Tempo de execu��o: '), write(T), write('s'),nl.


try_each_permutation(Room, Day, LOpCode) :-
    permutation(LOpCode, Permuted),
    \+attempt_schedule(Room, Day, Permuted), % Falhou? Impede novas tentativas.
    !, % Corta futuras execu��es de permuta��o.
    fail. % Indica que todo o processo yey.

try_each_permutation(Room, Day, LOpCode) :-
    permutation(LOpCode, Permuted),
    \+attempt_schedule(Room, Day, Permuted), fail. % Bem-sucedido? Continua normalmente.

attempt_schedule(Room, Day, LOpCode) :-
    write('PermutedLOpCode='),write(LOpCode),nl,
    % Limpar estado tempor�rio antes de cada tentativa
    retractall(agenda_staff1(_,_,_)),
    retractall(agenda_operation_room1(_,_,_)),
    retractall(availability(_,_,_)),
    findall(_,(agenda_staff(D,Day,Agenda),assertz(agenda_staff1(D,Day,Agenda))),_),
    agenda_operation_room(Room,Day,Agenda),assert(agenda_operation_room1(Room,Day,Agenda)),
    findall(_,(agenda_staff1(D,Day,L),free_agenda0(L,LFA),adapt_timetable(D,Day,LFA,LFA2),assertz(availability(D,Day,LFA2))),_),
    % Tentar escalonar com a permuta��o atual
    \+heuristic_schedule(Room, Day, LOpCode),fail.

heuristic_schedule(_, _, []) :- fail,!.
heuristic_schedule(Room, Day, LOpCode) :-
    write('List='), write(LOpCode),nl,
    select_next_surgery(Day, LOpCode, OpCode),
    write(OpCode),nl,
    schedule_phases(OpCode, Room, Day),
    delete(LOpCode, OpCode, RemainingOpCodes),
    \+heuristic_schedule(Room, Day, RemainingOpCodes), fail.

% Seleciona a pr�xima cirurgia a ser escalonada com base na disponibilidade inicial dos m�dicos
select_next_surgery(Day, LOpCode, SelectedOpCode) :-
    findall((OpCode, EarliestTime), (
        member(OpCode, LOpCode),
        surgery_id(OpCode, OpType),
        surgery(OpType, TAnaesthesia, TSurgery, TCleaning),
        TotalTime is TAnaesthesia + TSurgery + TCleaning,
        assignment_surgery(OpCode, Doctor, 2),
        %write('Doctor for '), write(OpCode), write('='), write(Doctor),nl,
        availability(Doctor, Day, LA),
        earliest_sufficient_interval(TotalTime, LA, EarliestTime)
    ), Options),
    sort(2, @=<, Options, SortedOptions), % Ordena pelas janelas de tempo mais cedo
    SortedOptions = [(SelectedOpCode, _) | _].

% Encontra a primeira janela dispon�vel suficiente para uma cirurgia

%--------------------------------------GENERAL-METHODS------------------------------------------------------

free_agenda0([],[(0,1440)]).
free_agenda0([(0,Tfin,_)|LT],LT1):-!,free_agenda1([(0,Tfin,_)|LT],LT1).
free_agenda0([(Tin,Tfin,_)|LT],[(0,T1)|LT1]):- T1 is Tin-1,
    free_agenda1([(Tin,Tfin,_)|LT],LT1).

free_agenda1([(_,Tfin,_)],[(T1,1440)]):-Tfin\==1440,!,T1 is Tfin+1.
free_agenda1([(_,_,_)],[]).
free_agenda1([(_,T,_),(T1,Tfin2,_)|LT],LT1):-Tx is T+1,T1==Tx,!,
    free_agenda1([(T1,Tfin2,_)|LT],LT1).
free_agenda1([(_,Tfin1,_),(Tin2,Tfin2,_)|LT],[(T1,T2)|LT1]):-T1 is Tfin1+1,T2 is Tin2-1,
    free_agenda1([(Tin2,Tfin2,_)|LT],LT1).


adapt_timetable(D,Date,LFA,LFA2):-timetable(D,Date,(InTime,FinTime)),treatin(InTime,LFA,LFA1),treatfin(FinTime,LFA1,LFA2).

treatin(InTime,[(In,Fin)|LFA],[(In,Fin)|LFA]):-InTime=<In,!.
treatin(InTime,[(_,Fin)|LFA],LFA1):-InTime>Fin,!,treatin(InTime,LFA,LFA1).
treatin(InTime,[(_,Fin)|LFA],[(InTime,Fin)|LFA]).
treatin(_,[],[]).

treatfin(FinTime,[(In,Fin)|LFA],[(In,Fin)|LFA1]):-FinTime>=Fin,!,treatfin(FinTime,LFA,LFA1).
treatfin(FinTime,[(In,_)|_],[]):-FinTime=<In,!.
treatfin(FinTime,[(In,_)|_],[(In,FinTime)]).
treatfin(_,[],[]).

intersect_all_agendas([Name],Date,LA):-!,availability(Name,Date,LA).
intersect_all_agendas([Name|LNames],Date,LI):-
    availability(Name,Date,LA),
    intersect_all_agendas(LNames,Date,LI1),
    intersect_2_agendas(LA,LI1,LI).

intersect_2_agendas([],_,[]).
intersect_2_agendas([D|LD],LA,LIT):-	intersect_availability(D,LA,LI,LA1),
					intersect_2_agendas(LD,LA1,LID),
					append(LI,LID,LIT).

intersect_availability((_,_),[],[],[]).

intersect_availability((_,Fim),[(Ini1,Fim1)|LD],[],[(Ini1,Fim1)|LD]):-
		Fim<Ini1,!.

intersect_availability((Ini,Fim),[(_,Fim1)|LD],LI,LA):-
		Ini>Fim1,!,
		intersect_availability((Ini,Fim),LD,LI,LA).

intersect_availability((Ini,Fim),[(Ini1,Fim1)|LD],[(Imax,Fmin)],[(Fim,Fim1)|LD]):-
		Fim1>Fim,!,
		min_max(Ini,Ini1,_,Imax),
		min_max(Fim,Fim1,Fmin,_).

intersect_availability((Ini,Fim),[(Ini1,Fim1)|LD],[(Imax,Fmin)|LI],LA):-
		Fim>=Fim1,!,
		min_max(Ini,Ini1,_,Imax),
		min_max(Fim,Fim1,Fmin,_),
		intersect_availability((Fim1,Fim),LD,LI,LA).


min_max(I,I1,I,I1):- I<I1,!.
min_max(I,I1,I1,I).

remove_unf_intervals(_,[],[]).
remove_unf_intervals(TSurgery,[(Tin,Tfin)|LA],[(Tin,Tfin)|LA1]):-DT is Tfin-Tin+1,TSurgery=<DT,!,
    remove_unf_intervals(TSurgery,LA,LA1).
remove_unf_intervals(TSurgery,[_|LA],LA1):- remove_unf_intervals(TSurgery,LA,LA1).


schedule_first_interval(TSurgery,[(Tin,_)|_],(Tin,TfinS)):-
    TfinS is Tin + TSurgery - 1.

insert_agenda((TinS,TfinS,OpCode),[],[(TinS,TfinS,OpCode)]).
insert_agenda((TinS,TfinS,OpCode),[(Tin,Tfin,OpCode1)|LA],[(TinS,TfinS,OpCode),(Tin,Tfin,OpCode1)|LA]):-TfinS<Tin,!.
insert_agenda((TinS,TfinS,OpCode),[(Tin,Tfin,OpCode1)|LA],[(Tin,Tfin,OpCode1)|LA1]):-insert_agenda((TinS,TfinS,OpCode),LA,LA1).

insert_agenda_doctors(_,_,[]).
insert_agenda_doctors((TinS,TfinS,OpCode),Day,[Doctor|LDoctors]):-
    retract(agenda_staff1(Doctor,Day,Agenda)),
    insert_agenda((TinS,TfinS,OpCode),Agenda,Agenda1),
    assert(agenda_staff1(Doctor,Day,Agenda1)),
    insert_agenda_doctors((TinS,TfinS,OpCode),Day,LDoctors).

earliest_sufficient_interval(_, [], inf). % Nenhuma janela suficiente
earliest_sufficient_interval(TotalTime, [(Tin, Tfin) | _], Tin) :-
    Tfin - Tin + 1 >= TotalTime, !. % Janela suficiente encontrada
earliest_sufficient_interval(TotalTime, [_ | Rest], Earliest) :-
    earliest_sufficient_interval(TotalTime, Rest, Earliest).

% Rotaciona a lista de salas
rotate_list([H|T], Rotated):- append(T, [H], Rotated).
