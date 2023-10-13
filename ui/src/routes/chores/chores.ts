import { HomeChoreDto } from '../../nlp-api';

export const createBlankChore = () => {
	return new HomeChoreDto({
		choreDescription: '',
		choreId: 0,
		choreName: '',
		chorePoints: 1,
		completedCount: 0,
		dateAdded: new Date(),
		dateScheduled: new Date(),
		interval: '1',
		intervalModifier: 'Weeks',
		priority: 'low',
		roomId: 0,
		dateDeleted: undefined,
		dateDisabled: undefined,
		dateLastCompleted: undefined
	});
};

export const validateAddChore = (chore: HomeChoreDto) => {
	if (chore.choreName.length < 2) return false;
	if (chore.interval.length === 0) return false;
	if (chore.intervalModifier.length === 0) return false;
	if (chore.roomId <= 0) return false;
	if (chore.priority.length === 0) return false;
	return true;
};
