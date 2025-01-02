:-dynamic generations/1.
:-dynamic population/1.
:-dynamic prob_crossover/1.
:-dynamic prob_mutation/1.
:- dynamic agenda_staff/3.
:- dynamic agenda_staff1/3.
:-dynamic agenda_operation_room/3.
:-dynamic agenda_operation_room1/3.
:-dynamic better_sol/5.
:- dynamic final_time/1.

% surgery(SurgeryType,TAnesthesia,TSurgery,TCleaning).
surgery(so2,45,60,45).
surgery(so3,45,90,45).
surgery(so4,45,75,45).

% surgery_id(Id,SurgeryType).
surgery_id(so100001,so2).
surgery_id(so100002,so3).
surgery_id(so100003,so4).
surgery_id(so100004,so2).
%surgery_id(so100005,so4).

% surgeries(NSurgeries).
surgeries(4).

% agenda_staff(StaffCode,Day,ListOfOccupiedTimes)
agenda_staff(d001,20241028,[(1080,1140,c01)]).
agenda_staff(d002,20241028,[(850,900,m02)]).
agenda_staff(d003,20241028,[(760,790,m01)]).
agenda_staff(n001,20241028,[(750,790,m01)]).
agenda_staff(n002,20241028,[(950,980,m02)]).
agenda_staff(n003,20241028,[(1000,1050,m01)]).
%agenda_staff(d004,20241028,[(850,900,m02),(940,980,c04)]).
agenda_staff(d005,20241028,[(720,850,m01)]).
agenda_staff(m001,20241028,[]).
agenda_staff(m002,20241028,[]).

% timetable(StaffCode,Day,WorkTimeInterval)
timetable(d001,20241028,(280,1200)).
timetable(d002,20241028,(300,1440)).
timetable(d003,20241028,(320,1320)).
timetable(n001,20241028,(300,1200)).
timetable(n002,20241028,(350,1250)).
timetable(n003,20241028,(290,1440)).
%timetable(d004,20241028,(420,1020)).
timetable(d005,20241028,(300,1300)).
timetable(m001,20241028,(250,1000)).
timetable(m002,20241028,(390,1440)).

% staff(Code,StaffType,Specialization,ListOfSurgeryTypes)
staff(d001,doctor,orthopaedist,[so2,so3,so4]).
staff(d002,doctor,orthopaedist,[so2,so3,so4]).
staff(d003,doctor,orthopaedist,[so2,so3,so4]).
staff(n001,nurse,anaesthetist,[so2,so3,so4]).
staff(n002,nurse,instrumenting,[so2,so3,so4]).
staff(n003,nurse,circulating,[so2,so3,so4]).
%staff(d004,doctor,orthopaedist,[so2,so3,so4]).
staff(d005,doctor,anaesthetist,[so2,so3,so4]).
staff(m001,assistant,medicalaction,[so2,so3,so4]).
staff(m002,assistant,medicalaction,[so2,so3,so4]).

% assignment_surgery(Surgery,StaffCode,PhaseNumber)
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

% agenda_operation_room(Room,Day,ListOfOccupiedTimes)
agenda_operation_room(or1,20241028,[(520,579,so100000),(1000,1059,so099999)]).

% parameters initialization
initialize:-write('Number of new generations: '),read(NG), 			
    (retract(generations(_));true), asserta(generations(NG)),
	write('Population size: '),read(PS),
	(retract(population(_));true), asserta(population(PS)),
	write('Probability of crossover (%):'), read(P1),
	PC is P1/100, 
	(retract(prob_crossover(_));true), 	asserta(prob_crossover(PC)),
	write('Probability of mutation (%):'), read(P2),
	PM is P2/100, 
	(retract(prob_mutation(_));true), asserta(prob_mutation(PM)).

generate(Room,Day):-
    initialize,
    generate_population(Pop),
    write('Pop='),write(Pop),nl,
    evaluate_population(Pop,PopValue,Room,Day),
    write('PopValue='),write(PopValue),nl,
    order_population(PopValue,PopOrd),
    generations(NG),
    generate_generation(0,NG,PopOrd,Room,Day).

generate_population(Pop):-
    population(PopSize),
    surgeries(NumT),
    findall(Surgery,surgery_id(Surgery,_),SurgeryList),
    generate_population(PopSize,SurgeryList,NumT,Pop).

generate_population(0,_,_,[]):-!.
generate_population(PopSize,SurgeryList,NumT,[Ind|Rest]):-
    PopSize1 is PopSize-1,
    generate_population(PopSize1,SurgeryList,NumT,Rest),
    generate_individual(SurgeryList,NumT,Ind),
    not(member(Ind,Rest)).
generate_population(PopSize,SurgeryList,NumT,L):-
    generate_population(PopSize,SurgeryList,NumT,L).
    


generate_individual([G],1,[G]):-!.

generate_individual(SurgeryList,NumT,[G|Rest]):-
    NumTemp is NumT + 1, % to use with random
    random(1,NumTemp,N),
    remove(N,SurgeryList,G,NewList),
    NumT1 is NumT-1,
    generate_individual(NewList,NumT1,Rest).

remove(1,[G|Rest],G,Rest).
remove(N,[G1|Rest],G,[G1|Rest1]):- N1 is N-1,
            remove(N1,Rest,G,Rest1).

%--------------------EVALUATE ALL---------------------------------

evaluate_population([],[],_,_).
evaluate_population([Ind|Rest],[Ind*V|Rest1],Room,Day):-
    evaluate(Ind,V,Room,Day),
    evaluate_population(Rest,Rest1,Room,Day).

%----------------------EVALUATE DEFAULT---------------------


evaluate(Seq,V,Room,Day):- 
    retractall(agenda_operation_room1(Room,Day,_)),
    agenda_operation_room(Room,Day,StartAgenda), assertz(agenda_operation_room1(Room,Day,StartAgenda)),
    schedule_operations(Seq,Room,Day),
    agenda_operation_room1(Room,Day,Agenda),
    reverse(Agenda,AgendaR),
    evaluate_final_time(AgendaR,Seq,V).

schedule_operations([],_,_).
schedule_operations([Surgery|TOp],Room,Day):-
    surgery_id(Surgery,Type),
    surgery(Type, TA,TS,TC),
    Duration is TA+TS+TC,
    agenda_operation_room1(Room, Day, Agenda),
    free_agenda0(Agenda, FAgRoom),
    remove_unf_intervals(Duration, FAgRoom, LAPossibilities),
    schedule_first_interval(Duration, LAPossibilities, (TStart, _)),

    TEnd is TStart + Duration,
    retract(agenda_operation_room1(Room, Day, OldAgenda)),
    insert_agenda((TStart, TEnd, Surgery), OldAgenda, NewAgenda),
    assertz(agenda_operation_room1(Room,Day,NewAgenda)),
    schedule_operations(TOp,Room,Day).

%evaluate([ ],_,0,_,_).
%evaluate([T|Rest],Inst,V,Room,Day):-
%    surgery_id(T,Type,Due,Pen),
%    surgery(Type,TA,TS,TC),
%    Dur is TA+TS+TC,
%    FinInst is Inst+Dur,
%    evaluate(Rest,FinInst,VRest),
%    ((FinInst =< Due,!, VT is 0) ; (VT is (FinInst-Due)*Pen)),
%    V is VT+VRest.



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

%----------------------------------------------------------------------------------

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
    
generate_generation(G,G,Pop,_,_):-!,
	write('Generation '), write(G), write(':'), nl, write(Pop), nl.
generate_generation(N,G,Pop,Room,Day):-
	write('Generation '), write(N), write(':'), nl, write(Pop), nl,
    Pop = [Best|_], % Extract the best element
    write('Best Element: '), write(Best), nl, % Write the first element
    crossover(Pop,NPop1),
	mutation(NPop1,NPop),
    evaluate_population(NPop,NPopValue,Room,Day),
	order_population(NPopValue,NPopOrd),
    change_if_not_present(Best, NPopOrd, NPopFinal), 
	order_population(NPopFinal,NPopOrd2),
	N1 is N+1,
	generate_generation(N1,G,NPopOrd2,Room,Day).

generate_crossover_points(P1,P2):- generate_crossover_points1(P1,P2).

generate_crossover_points1(P1,P2):-
	surgeries(N),
	NTemp is N+1,
	random(1,NTemp,P11),
	random(1,NTemp,P21),
	P11\==P21,!,
	((P11<P21,!,P1=P11,P2=P21);P1=P21,P2=P11).
generate_crossover_points1(P1,P2):-
	generate_crossover_points1(P1,P2).


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
    surgeries(T),
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
    surgeries(NumT),
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
