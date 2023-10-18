import { HomeFloorDto } from '../../../nlp-api';

export const createBlankFloorDto = (homeId: number) =>
	new HomeFloorDto({
		dateAdded: new Date(),
		floorId: 0,
		floorName: '',
		homeId: homeId,
		dateDeleted: new Date()
	});

export const validateAddFloor = (floor: HomeFloorDto) => {
	if (!floor) return false;
	if (floor.homeId <= 0) return false;
	if (floor.floorName.length < 2) return false;
	return true;
};
