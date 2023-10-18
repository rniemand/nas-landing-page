import { HomeFloorDto } from '../../../nlp-api';

export const createBlankFloorDto = (homeId: number) =>
	new HomeFloorDto({
		dateAddedUtc: new Date(),
		floorId: 0,
		floorName: '',
		homeId: homeId,
		dateDeletedUtc: new Date()
	});

export const validateAddFloor = (floor: HomeFloorDto) => {
	if (!floor) return false;
	if (floor.homeId <= 0) return false;
	if (floor.floorName.length < 2) return false;
	return true;
};
