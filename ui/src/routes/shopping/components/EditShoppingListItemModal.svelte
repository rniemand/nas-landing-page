<script lang="ts">
	import {
		Button,
		Modal,
		ModalHeader,
		ModalBody,
		Form,
		ModalFooter,
		FormGroup,
		Col,
		Row,
		Label,
		Input
	} from 'sveltestrap';
	import StoreNameAutoComplete from './StoreNameAutoComplete.svelte';
	import CategoryAutoComplete from './CategoryAutoComplete.svelte';
	import ShoppingListItemAutoComplete from './ShoppingListItemAutoComplete.svelte';
	import { createNewShoppingListItem, validateEditShoppingListItem } from '../shopping';
	import { authContext } from '../../../utils/AppStore';
	import { onMount } from 'svelte';
	import type { WhoAmIResponse, ShoppingListItemDto } from '../../../nlp-api';

	let open = false;
	let canAdd: boolean = false;
	let userContext: WhoAmIResponse;
	let item: ShoppingListItemDto = createNewShoppingListItem(0);

	const toggle = () => {
		open = !open;
	};

	const editItem = async () => {
		console.log('editItem()');
	};

	onMount(() => {
		return authContext.subscribe((_whoAmI) => {
			if (!_whoAmI) return;
			userContext = _whoAmI;
		});
	});

	export const edit = (_item: ShoppingListItemDto) => {
		item = _item;
		toggle();
	};

	$: canAdd = validateEditShoppingListItem(item);
</script>

<div>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Edit Shopping List Item</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col xs="6">
						<FormGroup>
							<Label>Store</Label>
							<StoreNameAutoComplete bind:value={item.storeName} placeholder="Store Name" />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Category</Label>
							<CategoryAutoComplete bind:value={item.category} placeholder="Item Category" />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<Col xs="9">
						<FormGroup>
							<Label>Item</Label>
							<ShoppingListItemAutoComplete bind:value={item.itemName} placeholder="Item Name" />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Qty.</Label>
							<Input type="number" min={1} bind:value={item.quantity} />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<FormGroup>
						<Label>Last Known Price</Label>
						<Input type="number" bind:value={item.lastKnownPrice} />
					</FormGroup>
				</Row>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled={!canAdd} on:click={editItem}>Save Changes</Button>
		</ModalFooter>
	</Modal>
</div>
