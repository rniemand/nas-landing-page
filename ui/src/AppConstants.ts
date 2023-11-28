import {
	ChoreUrls,
	ConfigUrls,
	GamesUrls,
	LinkUrls,
	ShoppingUrls,
	TasksUrls
} from './enums/AppUrls';
import type { NlpPlugin } from './modals/NlpPlugin';

export const AppPlugins: NlpPlugin[] = [
	{ icon: 'bi-check2-circle', name: 'Chores', url: ChoreUrls.Root },
	{ icon: 'bi-gear-fill', name: 'Config', url: ConfigUrls.Root },
	{ icon: 'bi-controller', name: 'Games', url: GamesUrls.Root },
	{ icon: 'bi-link-45deg', name: 'Links', url: LinkUrls.Root },
	{ icon: 'bi-cart', name: 'Shopping', url: ShoppingUrls.Root },
	{ icon: 'bi-check-all', name: 'Tasks', url: TasksUrls.Root }
];
