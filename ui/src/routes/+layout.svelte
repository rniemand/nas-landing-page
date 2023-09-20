<script>
	import { AuthClient } from '../nlp-api';
	import { updateAuthContext } from '../utils/AppStore';
	import { goto } from '$app/navigation';
  import 'bootstrap/dist/css/bootstrap.min.css';

	(async () => {
		const authResponse = await new AuthClient().challenge(false);
		updateAuthContext(authResponse);
		if (!authResponse?.signedIn) goto('/login');
	})();
</script>

<slot />
