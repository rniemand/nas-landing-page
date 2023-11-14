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
	import { createNewShoppingListItem, validateBoughtShoppingListItem } from '../shopping';
	import { authContext } from '../../../utils/AppStore';
	import { onMount } from 'svelte';
	import {
		type WhoAmIResponse,
		type ShoppingListItemDto,
		ShoppingListClient
	} from '../../../nlp-api';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	let open = false;
	let canSave: boolean = false;
	let userContext: WhoAmIResponse;
	let item: ShoppingListItemDto = createNewShoppingListItem(0);
	export let onItemBought: () => void;

	const toggle = () => {
		open = !open;
	};

	const markItemBought = async () => {
		const response = await new ShoppingListClient().markItemBought(item);
		if (!response.success) {
			toastError('Update Failed', response.error || 'Failed to update item');
			return;
		}
		toastSuccess('Updated', `Marked '${item.itemName}' as bought`);
		toggle();
		onItemBought();
	};

	onMount(() => {
		return authContext.subscribe((_whoAmI) => {
			if (!_whoAmI) return;
			userContext = _whoAmI;
		});
	});

	export const markBought = (_item: ShoppingListItemDto) => {
		item = _item;
		item.lastKnownPrice = item.lastKnownPrice || 0;
		toggle();
	};

	$: canSave = validateBoughtShoppingListItem(item);
</script>

<div>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Item Bought</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col>
						<FormGroup>
							<Label>Qty.</Label>
							<Input type="number" min={1} bind:value={item.quantity} />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Bought Price</Label>
							<Input type="number" bind:value={item.lastKnownPrice} />
						</FormGroup>
					</Col>
				</Row>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled={!canSave} on:click={markItemBought}>Mark Bought</Button>
		</ModalFooter>
	</Modal>
</div>
