<script>
	import { AuthClient } from '../nlp-api';
	import 'bootstrap/dist/css/bootstrap.min.css';
	import TopNavigation from '../components/TopNavigation.svelte';
	import { Styles } from 'sveltestrap';
	import { Container } from 'sveltestrap';
	import ToastManager from '../components/ToastManager.svelte';
	import 'bootstrap-icons/font/bootstrap-icons.css';
	import { setContext } from 'svelte';
	import { AppContext } from '../enums/AppContext';
	import { AppPlugins } from '../AppConstants';
	import { writable } from 'svelte/store';

	const userContext = writable();
	setContext(AppContext.Plugins, AppPlugins);
	setContext(AppContext.User, userContext);

	(async () => {
		const authResponse = await new AuthClient().challenge(false);
		$userContext = authResponse;
	})();
</script>

<Styles />
<TopNavigation />
<ToastManager />
<Container class="mt-2">
	<slot />
</Container>
