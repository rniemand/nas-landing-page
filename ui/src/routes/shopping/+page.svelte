<script lang="ts">
	import { Row, Col } from 'sveltestrap';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import { AppUrls } from '../../enums/AppUrls';
	import AddShoppingListItemModal from './components/AddShoppingListItemModal.svelte';
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Shopping" />
</NavigationCrumbs>

<Row>
	<Col>
		<div class="text-end">
			<AddShoppingListItemModal />
		</div>
		<div class="tasks mt-2">Something</div>
	</Col>
</Row>
