import { Component, EventEmitter, Output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-task-card',
  standalone: true,
  imports: [FormsModule, CommonModule],
  template: `
    <div class="add-task-card">
      <h3>Add New Task</h3>
      <div class="form-group">
        <label for="taskTitle">Task Title:</label>
        <input
          type="text"
          id="taskTitle"
          [(ngModel)]="taskTitle"
          placeholder="Enter task title..."
          class="task-input"
          (keyup.enter)="onSubmit()"
        />
      </div>
      <div class="button-group">
        <button
          type="button"
          class="submit-btn"
          (click)="onSubmit()"
          [disabled]="!taskTitle().trim()"
        >
          Add Task
        </button>
        <button
          type="button"
          class="cancel-btn"
          (click)="onCancel()"
        >
          Cancel
        </button>
      </div>
    </div>
  `,
  styleUrl: './add-task-card.component.css'
})
export class AddTaskCardComponent {
  @Output() taskAdded = new EventEmitter<string>();
  @Output() cancelled = new EventEmitter<void>();

  protected taskTitle = signal('');

  onSubmit() {
    const title = this.taskTitle().trim();
    if (title) {
      this.taskAdded.emit(title);
      this.taskTitle.set('');
    }
  }

  onCancel() {
    this.taskTitle.set('');
    this.cancelled.emit();
  }
}