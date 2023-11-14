import { ShoppingListItemDto } from '../../nlp-api';

export const validateAddShoppingListItem = (item: ShoppingListItemDto) => {
	if (item.homeId <= 0) return false;
	if (item.quantity <= 0) return false;
	if (item.storeName.length < 2) return false;
	if (item.itemName.length < 2) return false;
	return true;
};

export const validateEditShoppingListItem = (item: ShoppingListItemDto) => {
	return validateAddShoppingListItem(item);
};

export const validateBoughtShoppingListItem = (item: ShoppingListItemDto) => {
	return validateAddShoppingListItem(item);
};

export const createNewShoppingListItem = (homeId: number) => {
	return new ShoppingListItemDto({
		addedByUserId: 0,
		dateAdded: new Date(),
		homeId: homeId,
		itemId: 0,
		itemName: '',
		quantity: 1,
		storeName: '',
		category: '',
		dateDeleted: undefined,
		datePurchased: undefined,
		lastKnownPrice: 0
	});
};
