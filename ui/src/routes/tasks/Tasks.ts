import { UserTaskDto } from '../../nlp-api';

export const createNewTask = () =>
	new UserTaskDto({
		taskID: 0,
		dateAdded: new Date(),
		taskCategory: '',
		taskDescription: '',
		taskName: '',
		taskPriority: 128,
		taskSubCategory: '',
		userID: 0,
		dateCompleted: undefined,
		dateDeleted: undefined
	});

export const validateTaskForAdding = (task: UserTaskDto | undefined) => {
	if (!task) return false;
	if (task.taskCategory.length === 0) return false;
	if (task.taskSubCategory.length === 0) return false;
	if (task.taskName.length === 0) return false;
	if (task.taskPriority === -1) return false;
	return true;
};
