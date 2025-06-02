import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { DeveloperSkills, SkillsLevel, CreateDeveloperRequest, Developer } from '../../../services/developer.service';

@Component({
  selector: 'app-add-developer',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './add-developer.component.html',
  styleUrls: ['./add-developer.component.css']
})
export class AddDeveloperComponent implements OnInit {
  @Input() developer: Developer | null = null;
  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<CreateDeveloperRequest>();

  developerForm: FormGroup;
  skills = Object.keys(DeveloperSkills).filter(key => isNaN(Number(key)));
  skillLevels = Object.keys(SkillsLevel).filter(key => isNaN(Number(key)));

  constructor(private fb: FormBuilder) {
    this.developerForm = this.createForm();
  }

  ngOnInit() {
    if (this.developer) {
      // Заполняем форму данными разработчика
      this.developerForm.patchValue({
        fullName: this.developer.name,
        login: this.developer.login,
        experience: this.developer.experience,
        dailyCapacity: this.developer.dailyCapacity
      });

      // Очищаем существующие навыки
      while (this.skillsFormArray.length) {
        this.skillsFormArray.removeAt(0);
      }

      // Добавляем существующие навыки
      Object.entries(this.developer.skills).forEach(([skill, level]) => {
        this.skillsFormArray.push(this.fb.group({
          skill: [skill, Validators.required],
          level: [level, Validators.required]
        }));
      });
    } else {
      // Генерируем пароль только для нового разработчика
      this.generatePassword();
    }

    // Подписываемся на изменения полного имени для генерации логина
    this.developerForm.get('fullName')?.valueChanges.subscribe(name => {
      if (name && !this.developer) { // Генерируем логин только для нового разработчика
        const login = 'dev.' + name.toLowerCase().replace(/\s+/g, '.');
        this.developerForm.patchValue({ login });
      }
    });
  }

  private createForm(): FormGroup {
    return this.fb.group({
      fullName: ['', Validators.required],
      login: [{value: '', disabled: true}],
      password: [{value: '', disabled: true}],
      skills: this.fb.array([this.createSkillGroup()]),
      experience: ['', [Validators.required, Validators.min(0)]],
      dailyCapacity: ['', [Validators.required, Validators.min(1), Validators.max(24)]]
    });
  }

  createSkillGroup(): FormGroup {
    return this.fb.group({
      skill: ['', Validators.required],
      level: ['', Validators.required]
    });
  }

  get skillsFormArray() {
    return this.developerForm.get('skills') as FormArray;
  }

  addSkill() {
    this.skillsFormArray.push(this.createSkillGroup());
  }

  removeSkill(index: number) {
    this.skillsFormArray.removeAt(index);
  }

  generatePassword() {
    const length = 12;
    const charset = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*';
    let password = '';
    for (let i = 0; i < length; i++) {
      const randomIndex = Math.floor(Math.random() * charset.length);
      password += charset[randomIndex];
    }
    this.developerForm.patchValue({ password });
  }

  onSubmit() {
    if (this.developerForm.valid) {
      const formValue = this.developerForm.getRawValue();
      const skills = {} as { [key: string]: number };
      
      formValue.skills.forEach((skill: { skill: string, level: string }) => {
        skills[skill.skill] = SkillsLevel[skill.level as keyof typeof SkillsLevel];
      });

      const developerData: CreateDeveloperRequest = {
        id: this.developer?.id || '',
        name: formValue.fullName,
        login: formValue.login,
        password: formValue.password,
        skills: skills,
        experience: formValue.experience,
        dailyCapacity: formValue.dailyCapacity,
        registrationDate: this.developer?.registrationDate || new Date(),
        companyId: this.developer?.companyId || '',
        projectIds: this.developer?.projectIds || [],
        schedule: this.developer?.schedule || {}
      };

      this.save.emit(developerData);
    }
  }

  onClose() {
    this.close.emit();
  }
} 