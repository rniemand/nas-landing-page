<script lang="ts">
	import { Row, Col, Accordion, AccordionItem } from 'sveltestrap';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import { AppUrls } from '../../enums/AppUrls';
	import AddShoppingListItemModal from './components/AddShoppingListItemModal.svelte';
	import { ShoppingListClient, ShoppingListItemDto } from '../../nlp-api';
	import ShoppingListItemInfo from './components/ShoppingListItemInfo.svelte';
	import EditShoppingListItemModal from './components/EditShoppingListItemModal.svelte';

	let items: ShoppingListItemDto[] = [];
	let loading: boolean = false;
	let editModal: EditShoppingListItemModal;

	const refreshShoppingList = async () => {
		console.log('refreshShoppingList()');
		loading = true;
		items = await new ShoppingListClient().getShoppingList();
		loading = false;
	};

	const onEditItem = (item: ShoppingListItemDto) => {
		console.log('onEditItem', item);
		editModal.edit(item);
	};

	refreshShoppingList();
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Shopping" />
</NavigationCrumbs>

<Row>
	<Col>
		<div class="text-end">
			<AddShoppingListItemModal onItemAdded={refreshShoppingList} />
			<EditShoppingListItemModal bind:this={editModal} />
		</div>

		{#if !loading && items.length > 0}
			<div class="mt-2">
				<Accordion>
					{#each items as item}
						<AccordionItem>
							<span class="m-0" slot="header">
								{item.itemName}
							</span>
							<ShoppingListItemInfo {item} {onEditItem} />
						</AccordionItem>
					{/each}
				</Accordion>
			</div>
		{:else}
			<div>You have no shopping list items</div>
		{/if}
	</Col>
</Row>
