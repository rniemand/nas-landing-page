export class AppUrls {
	public static readonly Login = '/login';
	public static readonly Home = '/';
}

export class ChoreUrls {
	public static readonly Root = '/chores';
}

export class ConfigUrls {
	public static readonly Root = '/config';
	public static readonly Floors = `${ConfigUrls.Root}/floors`;
	public static readonly Rooms = `${ConfigUrls.Root}/rooms`;
	public static readonly Homes = `${ConfigUrls.Root}/homes`;
	public static FloorRooms = (floorID: number) => `${ConfigUrls.Rooms}?floorId=${floorID}`;
	public static Floor = (floorID: number) => `${ConfigUrls.Floors}?floorId=${floorID}`;
}

export class GamesUrls {
	public static readonly Root = '/games';
}

export class LinkUrls {
	public static readonly Root = '/links';
}

export class TasksUrls {
	public static readonly Root = '/tasks';
}

export class ShoppingUrls {
	public static readonly Root = '/shopping';
}
