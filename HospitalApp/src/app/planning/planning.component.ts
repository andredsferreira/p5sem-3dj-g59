import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PlanningService } from './planning-service';
import { Assignment, Operation, RoomSchedule, SchedulerResponse, StaffSchedules } from './schedule-type';
import { OperationTypeService } from '../operation-types/operation-type.service';

@Component({
  selector: 'app-planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.css'],
  imports: [CommonModule, FormsModule],
  standalone: true
})
export class PlanningComponent {
  isModalOpen = false;
  isScheduleMenuOpen = false;
  currentOperation: Operation = { name: '',staffList: []};
  operations: Operation[] = [];  
  rooms: string[] = [];
  operationTypes: any[] = [];
  deadline: string | null = null;
  assignmentOutput: Assignment[] | null = null;
  scheduleOutput: SchedulerResponse | null = null;
  parsedRoomSchedule: RoomSchedule | null = null;
  parsedStaffSchedules: StaffSchedules | null = null;
  showGeneticSettings = false;
  chosenAlgorithm: string | null = null;
  chosenRoom: string | null = null;
  geneticSettings = {
    populationSize: 5,
    mutationRate: 1,
    crossoverRate: 80,
    generations: 4,
  };

  constructor(private service : PlanningService) {}
  newStaffMember = '';
  roomName = '';
  operationName = '';

  addStaff() {
    if (this.newStaffMember.trim()) {
      this.currentOperation.staffList.push(this.newStaffMember.trim());
      this.newStaffMember = '';
    }
  }

  addOperation() {
    if (
      this.currentOperation.staffList.length > 0
    ) {
      this.currentOperation.name = this.operationName.trim();
      this.operations.push({ ...this.currentOperation });
      this.currentOperation = { name: '',staffList: [] };
    }
  }

  addRoom() {
    if (this.roomName.trim()) {
      this.rooms.push(this.roomName.trim());
      this.roomName = '';
    }
  }

  async assignSurgeries(){
    const output = await this.service.assignSurgeriesToRooms();
    this.assignmentOutput = this.parseAssignments(output['surgeries-per-room']);
    console.log(this.assignmentOutput);
  }

  parseAssignments(input: string): Assignment[] {
    console.log("Input string:", input); // Debugging
    const items = input.slice(1, -1).split("),(");
  
    return items.map((item) => {
      const match = item.match(/^\(?([a-zA-Z0-9]+),\[(.*?)\]\)?$/);
      if (match) {
        const room = match[1];
        const surgeries = match[2].split(",").map((s) => s.trim()); // Trim spaces
        return { room, surgeries };
      }
  
      console.error("Invalid format for item:", item);
      throw new Error(`Invalid format for item: ${item}`);
    });
  }

  async getAll(room:string) {
    console.log("Room=",room)
    this.scheduleOutput = await this.service.scheduleSurgeriesObtainBetter(room);

    console.log(`final time = ${this.scheduleOutput['final-time']}`)
    this.parseSchedulerResponse();
  }

  async heuristic(room:string) {
    this.scheduleOutput = await this.service.scheduleSurgeriesGenetic(room,2,3,70,4);

    console.log(`final time = ${this.scheduleOutput['final-time']}`)
    this.parseSchedulerResponse();
  }

  parseSchedulerResponse() {
    if (!this.scheduleOutput) return;

    // Parse "room-schedule"
    this.parsedRoomSchedule = {
      values: this.scheduleOutput['room-schedule']
        .match(/\(([^)]+)\)/g)
        ?.map((frame) => {
          const [minute_i, minute_f, operation] = frame
            .slice(1, -1)
            .split(',');
          return {
            minute_i: parseInt(minute_i, 10),
            minute_f: parseInt(minute_f, 10),
            operation
          };
        }) || []
    };

    // Parse "staff-schedules"
    this.parsedStaffSchedules = {
      staffs: this.scheduleOutput['staff-schedules']
        .match(/\(([^)]+),\[(.*?)\]\)/g)
        ?.map((entry) => {
          const [staff, frames] = entry
            .slice(1, -1)
            .split(',[');

          const parsedFrames = frames
            .match(/\(([^)]+)\)/g)
            ?.map((frame) => {
              const [minute_i, minute_f, operation] = frame
                .slice(1, -1)
                .split(',');
              return {
                minute_i: parseInt(minute_i, 10),
                minute_f: parseInt(minute_f, 10),
                operation
              };
            }) || [];

          return {
            staff,
            values: parsedFrames
          };
        }) || []
    };
  }
  async genetic(room: string) {
    this.scheduleOutput = await this.service.scheduleSurgeriesGenetic(room,this.geneticSettings.generations,this.geneticSettings.populationSize,this.geneticSettings.crossoverRate,this.geneticSettings.mutationRate);

    console.log(`final time = ${this.scheduleOutput['final-time']}`)
    this.parseSchedulerResponse();
  }

  async chooseAlgorithm(room: string) {
    const now = new Date();
    const deadlineDate = new Date(this.deadline!);
  
    if (deadlineDate > now) {
      // Calculate the time difference in seconds
      const diffInMS = deadlineDate.getTime() - now.getTime();
      const time = Math.floor(diffInMS / 1000);
  
      try {
        // Call the service to decide the best method
        const response = (await this.service.decideBestMethod(room, time)).method;
  
        // Set chosenAlgorithm based on the response
        if (response === 'obtain_better_sol') {
          this.chosenAlgorithm = 'Gerar todas e obter a melhor';
        } else if (response === 'heuristic') {
          this.chosenAlgorithm = 'Heurística';
        } else if (response.startsWith('generate')) {
          const match = response.match(/Population = (\d+), Generations = (\d+)/);
  
          if (match) {
            const [, population, generations] = match.map(Number); // Extract numbers from the response
            this.chosenAlgorithm = 'Algoritmo Genético';
            this.geneticSettings.populationSize = population;
            this.geneticSettings.generations = generations;
          } else {
            console.error('Unexpected format for genetic algorithm response:', response);
          }
        } else {
          console.error('Unexpected response:', response);
        }
      } catch (error) {
        console.error('Error deciding best method:', error);
      }
    } else {
      console.error('Deadline has to be after the current moment');
    }
  }
  
}
