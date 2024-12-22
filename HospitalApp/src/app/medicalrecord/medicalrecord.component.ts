import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MedicalRecordService } from './medicalrecord-service';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FamilyHistoryEntry } from './entry-types';

interface Field {
  selected: boolean;
  value: string;
}

@Component({
  selector: 'app-medicalrecord',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medicalrecord.component.html',
  styleUrl: './medicalrecord.component.css'
})
export class MedicalRecordComponent implements OnInit {

  record: string | null = null;
  recordDetails: any = null;
  recordNotFound = false;
  token: string | null = null;
  errorMessage: string | null = null;
  showPage = false;
  selectedItem: FamilyHistoryEntry | null = null;
  isSearching = false;
  isCreating = false;
  isEditing = false;

  activeTabIndex: number | null = null; // Índice da aba ativa
  showSearchMenus: { [key: string]: boolean } = { familyHistory: false, allergies: false };
  familyHistoryResults: FamilyHistoryEntry[] | null = [];
  searchCriteria: { [key: string]: any } = { familyHistory: { date: '', condition: '' } };

  attributes = [
    { key: 'relative', label: 'Familiar' },
    { key: 'history', label: 'Detalhes' }
  ];
  fields: { [key: string]: Field } = {};

  constructor(private route: ActivatedRoute, private medicalRecordService: MedicalRecordService, private authService: AuthService) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
  
    if (!this.token) {
      this.errorMessage = 'Você não tem as permissões necessárias para aceder esta página.';
      return;
    }
  
    const role = this.authService.getRoleFromToken(this.token);
    if (role !== "Admin" && role !== "Doctor") {
      this.errorMessage = 'Você não tem as permissões necessárias para aceder esta página.';
      return;
    }
  
    this.record = this.route.snapshot.paramMap.get('id');
    if (!this.record) {
      this.errorMessage = 'Registo médico não foi inserido.';
      return;
    }
  
    this.validateRecord(this.record);

    this.attributes.forEach((attr) => {
      this.fields[attr.key] = { selected: false, value: '' };
    });
  }
  
  private async validateRecord(id: string): Promise<void> {
    try {
      const record = await this.fetchRecord(id);
      if (record === null) {
        this.errorMessage = 'Este registo médico não existe.';
        return;
      }
      this.showPage = true;
    } catch (error) {
      this.errorMessage = 'Erro ao validar o registo médico.';
      console.error(error);
    }
  }
  
  async fetchRecord(id: string): Promise<string | null> {
    try {
      const result = await this.medicalRecordService.getMedicalRecord(this.token, id);
      return result.body || null;
    } catch (error) {
      console.error("Erro ao buscar o registo médico:", error);
      return null;
    }
  }  

  resetFields() {
    this.fields = this.attributes.reduce(
      (fields, attr) => {
        fields[attr.key] = { selected: false, value: '' };
        return fields;
      },
      {} as { [key: string]: Field }
    );
  }

  startCreate() : void {
    this.resetFields();
    this.isCreating = true;
  }

  async onCreate() : Promise<void> {
    const createParams: {relative:string, history:string} = {relative: '',history: ''};
    for (const [key, field] of Object.entries(this.fields)) {
      createParams[key as keyof {relative:string, history:string}] = field.value;
    }
    try{
      const response = await this.medicalRecordService.createFamilyEntry(this.token, this.record!, createParams);
      //this.showMessage = true;
      //if (response) {
      //  this.messageText = `Paciente ${response.body?.MedicalRecordNumber} criado com sucesso!`;
      //  this.messageClass = 'bg-green-500 text-white';
      //}
      this.familyHistoryResults?.push(response.body!);
      
    } catch (error) {
      console.error("Erro ao criar paciente:", error);
      //this.showMessage = true;
      //this.messageText = 'Erro ao criar paciente. Tente novamente.';
      //this.messageClass = 'bg-red-500 text-white';
    } finally {
      this.isCreating = false;
      this.selectedItem = null;
      //await this.loadPatients();

      //setTimeout(() => {
      //  this.showMessage = false;
      //}, 3000);
    }
  }
  
  cancelCreate(){
    this.isCreating = false;
  }

  toggleTab(index: number): void {
    this.activeTabIndex = this.activeTabIndex === index ? null : index;
  }

  toggleSearchMenu(index: number): void {
    this.showSearchMenus[index] = !this.showSearchMenus[index];
  }

  listFamilyHistory(): void {
    this.resetFields();
    this.isSearching = true;
  }

  async submitSearch(): Promise<void> {
    const searchParams: {relative?:string, history?: string} = {};

    for (const [key, field] of Object.entries(this.fields)) {
      if (field.selected) {
        searchParams[key as keyof {relative:string, history: string}] = field.value;
      }
    }

    try{
      var result = await this.medicalRecordService.getFamilyHistoryEntries(this.token, this.record!, searchParams);
      this.familyHistoryResults = result.body;
      console.log("listing successful")
    } catch (error) {
      console.log("oops");
    } finally {
      this.isSearching = false;
    }
  }

  cancelSearch() : void {
    this.isSearching = false;
  }

  onEdit(entry: FamilyHistoryEntry): void {
    this.resetFields();
    this.selectedItem = { ...entry };
    this.isEditing = true;
    
    this.attributes.forEach((attr) => {
      const value = entry[attr.key as keyof FamilyHistoryEntry] || '';
      this.fields[attr.key].value = value;
    });
  }

  async submitEdit(entry: FamilyHistoryEntry | null): Promise<void> {
    const searchParams: {relative?:string, history?: string} = {};

    for (const [key, field] of Object.entries(this.fields)) {
      if (field.selected) {
        searchParams[key as keyof {relative:string, history: string}] = field.value;
      }
    }

    try{
      var result = await this.medicalRecordService.editFamilyHistoryEntry(this.token, entry!.entryNumber, searchParams);
      console.log("edit successful")
      const entryToChange = this.familyHistoryResults?.find(e => e.entryNumber === entry!.entryNumber);
      if (searchParams.history) entryToChange!.history = searchParams.history; 
      if (searchParams.relative) entryToChange!.relative = searchParams.relative; 
    } catch (error) {
      console.log("oops");
    } finally {
      this.isEditing = false;
      this.resetFields();
    }
  }

  // Cancel edit operation
  cancelEdit(): void {
    this.isEditing = false;
  }
}
