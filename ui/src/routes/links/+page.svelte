<script lang="ts">
	import UserLinks from '../../components/UserLinks/UserLinks.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import { AppUrls } from '../../enums/AppUrls';
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Links" />
</NavigationCrumbs>

<UserLinks />
