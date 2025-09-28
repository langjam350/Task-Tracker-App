import ITaskController from '../controllers/ITaskController';
import ITask from '../models/ITask';

export default class TaskController implements ITaskController {
    private getApiBaseUrl(): string {
        // In development, use localhost. In production, use relative URLs
        return window.location.hostname === 'localhost' ? 'https://localhost:7235' : '';
    }

    public async getAllTasks(): Promise<ITask[]> {
        try {
            const response = await fetch(`${this.getApiBaseUrl()}/api/task`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error fetching tasks:', error);
            return [];
        }
    }

    public async createTask(task: ITask): Promise<boolean> {
        try {
            const response = await fetch(`${this.getApiBaseUrl()}/api/task`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(task)
            });
            return response.ok;
        } catch (error) {
            console.error('Error creating task:', error);
            return false;
        }
    }

    public async completeTask(taskId: string): Promise<void> {
        try {
            const response = await fetch(`${this.getApiBaseUrl()}/api/task/${taskId}/complete`, {
                method: 'PUT'
            });
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
        } catch (error) {
            console.error('Error completing task:', error);
            throw error;
        }
    }
}