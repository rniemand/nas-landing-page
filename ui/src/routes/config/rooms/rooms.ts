import { HomeRoomDto } from '../../../nlp-api';

export const createBlankRoom = (floorId: number) =>
	new HomeRoomDto({
		dateAdded: new Date(),
		floorId: floorId,
		roomId: 0,
		roomName: '',
		dateDeleted: undefined
	});

export const validateAddRoom = (room: HomeRoomDto) => {
	if (!room) return false;
	if (room.floorId <= 0) return false;
	if (room.roomName.length < 2) return false;
	return true;
};
