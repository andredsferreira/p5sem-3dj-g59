:-dynamic agenda_operation_room/3.
:-dynamic agenda_operation_room1/3.
:-dynamic total_surgery_time/2.

% surgery(SurgeryType,TAnesthesia,TSurgery,TCleaning).
surgery(so2,45,60,45).
surgery(so3,45,90,45).
surgery(so4,45,75,45).

% surgery_id(Id,SurgeryType).
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

% surgeries(NSurgeries).
surgeries(13).

% agenda_operation_room(Room,Day,ListOfOccupiedTimes)
agenda_operation_room(or1,20241028,[(520,579,so100000),(1000,1059,so099999)]).
agenda_operation_room(or2,20241028,[(700,800,so200000)]).
agenda_operation_room(or3,20241028,[(650,800,so300000),(950,1050,so088888)]).

% Função principal
associate_surgeries_to_rooms(Day):-
    retractall(total_surgery_time(_,_)),
    retractall(agenda_operation_room1(_,_,_)),
    findall(_, (agenda_operation_room(R,D,Agenda), assertz(agenda_operation_room1(R,D,Agenda))), _),
    findall(SurgeryID, surgery_id(SurgeryID, _), LSurgeries),
    getTotalTimes(LSurgeries),
    sort_surgeries_by_time(SortedSurgeries),
    findall(Room, agenda_operation_room1(Room, Day, _), LRooms),
    distribute_surgeries(SortedSurgeries, LRooms, Day).

% Calcula os tempos totais das cirurgias
getTotalTimes([]):- !.
getTotalTimes([Surgery|TSurgeries]):-
    surgery_id(Surgery, Type),
    surgery(Type, TimeA, TimeS, TimeC),
    Time is TimeA + TimeS + TimeC,
    assertz(total_surgery_time(Surgery, Time)),
    %write("Surgery "), write(Surgery), write(" has time="), write(Time), nl,
    getTotalTimes(TSurgeries).

% Ordena as cirurgias pelo tempo total necessário
sort_surgeries_by_time(SortedSurgeries):-
    findall((Time, Surgery), total_surgery_time(Surgery, Time), SurgeriesWithTimes),
    sort(1, @=<, SurgeriesWithTimes, SortedByTime),
    findall(Surgery, member((_, Surgery), SortedByTime), SortedSurgeries).

% Distribui as cirurgias nas salas rotativamente

distribute_surgeries([], _, _):- !.
distribute_surgeries([Surgery|RemainingSurgeries], Rooms, Day):-
    nth1(_, Rooms, Room),
    schedule_surgery(Surgery, Room, Day),
    rotate_list(Rooms, RotatedRooms),
    distribute_surgeries(RemainingSurgeries, RotatedRooms, Day).

% Tenta agendar a cirurgia em uma sala
schedule_surgery(Surgery, Room, Day):-
    total_surgery_time(Surgery, Duration),
    agenda_operation_room1(Room, Day, Agenda),
    free_agenda0(Agenda, FAgRoom),
    remove_unf_intervals(Duration, FAgRoom, LAPossibilities),
    schedule_first_interval(Duration, LAPossibilities, (TStart, _)),

    TEnd is TStart + Duration,
    retract(agenda_operation_room1(Room, Day, OldAgenda)),
    insert_agenda((TStart, TEnd, Surgery), OldAgenda, NewAgenda),
    assertz(agenda_operation_room1(Room,Day,NewAgenda)),
    write("Scheduled "), write(Surgery), write(" (with duration="), write(Duration), write(") in room "), write(Room), nl,
    write("Updated room "), write(Room), write("'s agenda: "), write(NewAgenda), nl.

insert_agenda((TinS,TfinS,OpCode),[],[(TinS,TfinS,OpCode)]).
insert_agenda((TinS,TfinS,OpCode),[(Tin,Tfin,OpCode1)|LA],[(TinS,TfinS,OpCode),(Tin,Tfin,OpCode1)|LA]):-TfinS<Tin,!.
insert_agenda((TinS,TfinS,OpCode),[(Tin,Tfin,OpCode1)|LA],[(Tin,Tfin,OpCode1)|LA1]):-insert_agenda((TinS,TfinS,OpCode),LA,LA1).

remove_unf_intervals(_,[],[]).
remove_unf_intervals(TSurgery,[(Tin,Tfin)|LA],[(Tin,Tfin)|LA1]):-DT is Tfin-Tin+1,TSurgery=<DT,!,
    remove_unf_intervals(TSurgery,LA,LA1).
remove_unf_intervals(TSurgery,[_|LA],LA1):- remove_unf_intervals(TSurgery,LA,LA1).


schedule_first_interval(TSurgery,[(Tin,_)|_],(Tin,TfinS)):-
    TfinS is Tin + TSurgery - 1.

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

% Rotaciona a lista de salas
rotate_list([H|T], Rotated):- append(T, [H], Rotated).