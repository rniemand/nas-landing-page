<script lang="ts">
	import {
		Button,
		Input,
		Form,
		FormGroup,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Label,
		Col,
		Row
	} from 'sveltestrap';
	let open = false;
	const toggle = () => (open = !open);
</script>

<div>
	<Button color="success" on:click={toggle}>Add Task</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Task</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col xs="9">
						<FormGroup>
							<Label for="name">Task Name</Label>
							<Input type="text" id="name" />
						</FormGroup>
					</Col>
					<Col>
						<Label for="priority">Priority</Label>
						<Input type="select" id="priority">
							<option>One</option>
							<option>Two</option>
						</Input>
					</Col>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label for="category">Category</Label>
							<Input type="text" id="category" />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label for="subCategory">Sub Category</Label>
							<Input type="text" id="subCategory" />
						</FormGroup>
					</Col>
				</Row>
				<FormGroup>
					<Label for="description">Task Description</Label>
					<Input type="textarea" id="description" />
				</FormGroup>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled on:click={toggle}>Add Task</Button>
		</ModalFooter>
	</Modal>
</div>
