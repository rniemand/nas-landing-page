<script>
	import { AuthClient } from '../nlp-api';
	import { updateAuthContext } from '../utils/AppStore';
	import { goto } from '$app/navigation';
	import 'bootstrap/dist/css/bootstrap.min.css';
	import TopNavigation from '../components/TopNavigation.svelte';
	import { Styles } from 'sveltestrap';
	import { Container } from 'sveltestrap';
	import ToastManager from '../components/ToastManager.svelte';
	import 'bootstrap-icons/font/bootstrap-icons.css';
	import { setContext } from 'svelte';
	import { AppContext } from '../enums/AppContext';
	import { AppPlugins } from '../AppConstants';

	setContext(AppContext.Plugins, AppPlugins);

	(async () => {
		const authResponse = await new AuthClient().challenge(false);
		updateAuthContext(authResponse);
		if (!authResponse?.signedIn) goto('/login');
	})();
</script>

<Styles />
<TopNavigation />
<ToastManager />
<Container class="mt-2">
	<slot />
</Container>
