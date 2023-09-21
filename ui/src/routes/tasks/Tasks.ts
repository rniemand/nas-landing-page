import { UserTaskDto } from '../../nlp-api';

export const createNewTask = () => {
	return new UserTaskDto({
		taskID: 0,
		dateAddedUtc: new Date(),
		taskCategory: '',
		taskDescription: '',
		taskName: '',
		taskPriority: 128,
		taskSubCategory: '',
		userID: 0,
		dateCompletedUtc: undefined,
		dateDeletedUtc: undefined
	});
};
