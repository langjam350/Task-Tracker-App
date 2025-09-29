import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AddTaskCardComponent } from '../components/add-task-card.component';
import TaskController from '../controllers/TaskController';
import ITask from '../models/ITask';

/**
 * Component prop for Application Task Tracker
 */
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, AddTaskCardComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

/**
 * Exports class definition for the Application Task Tracker
 */
export class App implements OnInit {
  // Defines readonly properties for use on page
  protected readonly title = signal('Task Tracker');
  protected readonly tasks = signal<ITask[]>([]);
  protected readonly showAddTaskCard = signal(false);
  protected readonly isLoading = signal(false);

  // Private task controller defined for use in Application
  private taskController = new TaskController();

  // OnInit lifecycle hook to load tasks when component initializes
  async ngOnInit() {
    await this.loadTasks();
  }

  // Loads tasks from the controller and updates the tasks signal
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

  // Display the Add Task Card to allow the user to add a new task
  showAddTask() {
    this.showAddTaskCard.set(true);
  }

  // Hide add task card after task is added or addition is cancelled
  hideAddTask() {
    this.showAddTaskCard.set(false);
  }

  // Handle the addition of a new task and refresh the task list
  async onTaskAdded(title: string) {
    const newTask: ITask = {
      id: 0,
      title,
      isCompleted: false
    };

    const success = await this.taskController.createTask(newTask);
    if (success) {
      await this.loadTasks();
      this.hideAddTask();
    } else {
      alert('Failed to add task. Please try again.');
    }
  }

  // Toggle if the task is complete and refresh the task list
  async toggleTaskCompletion(task: ITask) {
    if (!task.isCompleted) {
      try {
        await this.taskController.completeTask(task.id.toString());
        await this.loadTasks();
      } catch (error) {
        console.error('Error completing task:', error);
        alert('Failed to complete task. Please try again.');
      }
    }
  }
}
