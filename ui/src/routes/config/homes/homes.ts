import { HomeDto } from '../../../nlp-api';

export const createBlankHome = () =>
	new HomeDto({
		dateAdded: new Date(),
		homeId: 0,
		homeName: '',
		latitude: 0,
		longitude: 0,
		addressLine1: undefined,
		addressLine2: undefined,
		city: undefined,
		country: undefined,
		dateDeleted: undefined,
		postalCode: undefined,
		province: undefined
	});

export const validateAddHome = (home: HomeDto) => {
	if (!home) return false;
	if (home.homeName.length < 3) return false;
	if ((home?.addressLine1?.length || 0) < 2) return false;
	if ((home?.city?.length || 0) < 2) return false;
	if ((home?.country?.length || 0) < 2) return false;
	if ((home?.postalCode?.length || 0) < 2) return false;
	if ((home?.province?.length || 0) < 2) return false;
	return true;
};
