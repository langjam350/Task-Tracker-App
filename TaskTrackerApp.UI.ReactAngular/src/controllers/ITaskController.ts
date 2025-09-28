import ITask from '../models/ITask';

export default interface ITaskController {
    getAllTasks(): Promise<ITask[]>;
    createTask(task: ITask): Promise<boolean>;
    completeTask(taskId: string): Promise<void>;
}