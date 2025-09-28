import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AddTaskCardComponent } from '../components/add-task-card.component';
import TaskController from '../controllers/TaskController';
import ITask from '../models/ITask';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, AddTaskCardComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly title = signal('Task Tracker');
  protected readonly tasks = signal<ITask[]>([]);
  protected readonly showAddTaskCard = signal(false);
  protected readonly isLoading = signal(false);

  private taskController = new TaskController();

  async ngOnInit() {
    await this.loadTasks();
  }

  async loadTasks() {
    this.isLoading.set(true);
    try {
      const tasks = await this.taskController.getAllTasks();
      this.tasks.set(tasks);
    } catch (error) {
      console.error('Error loading tasks:', error);
    } finally {
      this.isLoading.set(false);
    }
  }

  showAddTask() {
    this.showAddTaskCard.set(true);
  }

  hideAddTask() {
    this.showAddTaskCard.set(false);
  }

  async onTaskAdded(title: string) {
    const newTask: ITask = {
      id: 0, // API will assign the ID
      title,
      isCompleted: false
    };

    const success = await this.taskController.createTask(newTask);
    if (success) {
      await this.loadTasks(); // Reload tasks to get the updated list
      this.hideAddTask();
    } else {
      alert('Failed to add task. Please try again.');
    }
  }

  async toggleTaskCompletion(task: ITask) {
    if (!task.isCompleted) {
      try {
        await this.taskController.completeTask(task.id.toString());
        await this.loadTasks(); // Reload tasks to get updated status
      } catch (error) {
        console.error('Error completing task:', error);
        alert('Failed to complete task. Please try again.');
      }
    }
  }
}
