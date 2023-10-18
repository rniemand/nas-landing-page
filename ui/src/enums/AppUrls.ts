export class AuthUrls {
	public static readonly Login = '/login';
}

export class ChoreUrls {
	public static readonly Root = '/chores';
}

export class ConfigUrls {
	public static readonly Root = '/config';
	public static readonly Floors = `${ConfigUrls.Root}/floors`;
	public static readonly Rooms = `${ConfigUrls.Root}/rooms`;
	public static FloorRooms = (floorID: number) => `${ConfigUrls.Rooms}?floorId=${floorID}`;
}

export class GamesUrls {
	public static readonly Root = '/games';
}

export class TasksUrls {
	public static readonly Root = '/tasks';
}
