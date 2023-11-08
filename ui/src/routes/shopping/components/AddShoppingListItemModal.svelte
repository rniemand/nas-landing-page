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

	let open = false;
	let canAdd: boolean = false;

	const toggle = () => {
		open = !open;
		// if (open) userTask = createNewTask();
	};

	const addShoppingListItem = () => {
		console.log('addShoppingListItem()');
	};
</script>

<div>
	<Button color="success" on:click={toggle}>Add Item</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Shopping List Item</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col xs="6">
						<FormGroup>
							<Label>Store</Label>
							<StoreNameAutoComplete />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Category</Label>
							<CategoryAutoComplete />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<Col xs="9">
						<FormGroup>
							<Label>Item</Label>
							<ShoppingListItemAutoComplete />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Qty.</Label>
							<Input type="number" min={1} />
						</FormGroup>
					</Col>
				</Row>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled={!canAdd} on:click={addShoppingListItem}>Add Item</Button>
		</ModalFooter>
	</Modal>
</div>
