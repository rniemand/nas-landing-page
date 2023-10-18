<script lang="ts">
	import { Row } from 'sveltestrap';
	import { AppUrls, ConfigUrls } from '../../enums/AppUrls';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import ItemButtons from '../../components/core/ItemButtons.svelte';
	import ItemButton from '../../components/core/ItemButton.svelte';
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Configuration" />
</NavigationCrumbs>

<Row>
	<ItemButtons>
		<ItemButton small icon="bi-building-fill-up" name="Floors" url={ConfigUrls.Floors} />
		<ItemButton small icon="bi-sort-numeric-up" name="Rooms" url={ConfigUrls.Rooms} />
	</ItemButtons>
</Row>
