<script>
	import { AuthClient } from '../nlp-api';
	import { updateAuthContext } from '../utils/AppStore';
	import { goto } from '$app/navigation';
	import 'bootstrap/dist/css/bootstrap.min.css';
	import TopNavigation from '../components/TopNavigation.svelte';
	import { Styles } from 'sveltestrap';
	import { Col, Container, Row } from 'sveltestrap';

	(async () => {
		const authResponse = await new AuthClient().challenge(false);
		updateAuthContext(authResponse);
		if (!authResponse?.signedIn) goto('/login');
	})();
</script>

<Styles />
<TopNavigation />
<Container>
	<slot />
</Container>
